/*
Paketin Protokol�:

Address FROM | Address TO | Control | Function | Length | Data | CRC

Address FROM : 1 byte   (Paketin olu�turulup yolland��� PIC adresi)
Address TO :   1 byte   (Paketin ula�aca�� PIC adresi)
Control :      1 byte   (ACK ve NACK i�lemleri)
Funtion :      1 byte   (Fonskiyon listesinden se�ilecek)
Length :       1 byte   (Data n�n uzunlu�unu belirtmek i�in)
Data :         0-N byte (�stendi�i kadar uzat�labilinen data)
CRC :          1 byte   (8 bit CRC)

Komut Listesi:

0x01- Motor ->Tam Tur Sa�
0x02- Motor ->Tam Tur Sol
0x03- Motor ->Yar�m Tur Sa�
0x04- Motor ->Yar�m Tur Sol
0x05- Motor ->Toggle Sa�
0x06- Motor ->Toggle Sol
0x07- Motor ->Motoru durdur
0x08- Motor ->H�z de�erini yazma
*/

#include <16F877.h>
#device ADC=16

#fuses XT,NOWDT,NOPROTECT,NOBROWNOUT,NOPUT,NOWRT,NODEBUG,NOCPD,NOLVP
#use delay(crystal=4Mhz)

#use rs232(baud=9600, xmit=pin_C6, rcv=pin_C7, parity=N, stop=1, TIMEOUT=10)

#include <crc.c>

#define MASTER 0x11
#define SLAVE1 0x22
#define SLAVE2 0x33
#define SLAVE3 0x44
#define ACK 0x01
#define NACK 0xFF
#define BUFFER_SIZE 64
#define CRC_KEY 0x55

#define gonder pin_C4
#define slv1_pin pin_A3
#define slv2_pin pin_A4
#define slv3_pin pin_A5

int buffer_tx[BUFFER_SIZE]; // Gonderilecek olan paket
int buffer_rx[BUFFER_SIZE]; // Ald���m�z paket
char i,j,k;
char P_Length, Error, new_packet;
char slv1_speed,slv2_speed,slv3_speed;
char slv1,slv2,slv3;

#int_RDA
void iletisim()
{
   new_packet=FALSE;
   buffer_rx[0] = getc(); // kimden
   buffer_rx[1] = getc(); // hedefimiz
   if(buffer_rx[1] == MASTER)
   {
      buffer_rx[2] = getc(); // ack
      buffer_rx[3] = getc(); // fonksiyon
      buffer_rx[4] = getc(); // datan�n uzunlu�u
      
      for(k=0; k<(buffer_rx[4]+1); k++) //data lar�n al�nmas�
         buffer_rx[k+5] = getc();
      
      P_Length = 6+buffer_rx[4]; // Paketin uzunlu�u

      Error = FALSE;
      if(buffer_rx[k+5] != generate_8bit_crc(buffer_rx, P_Length, CRC_KEY))
         Error = TRUE;
      
      if(buffer_rx[1] == MASTER && Error == FALSE)
         new_packet = TRUE;
   }
}

void send_packet(int* packet_ptr, int16 packet_length)
{
   int *ptr;
   int CRC, i;
   
   output_high(gonder);
   ptr = packet_ptr;
   
   CRC = generate_8bit_crc(ptr, packet_length, CRC_KEY);
   
   for(i=0; i<packet_length; i++)
      putc(packet_ptr[i]);
   clear_interrupt(INT_TBE);
   putc(CRC);
   
   while(!interrupt_active(INT_TBE));
   delay_ms(1);
   output_low(gonder);
}

void create_packet(char slv, char func, char len, char data)
{
   new_packet = FALSE;
   buffer_tx[0] = MASTER;
   buffer_tx[1] = slv;
   buffer_tx[2] = NACK;
   buffer_tx[3] = func;
   buffer_tx[4] = len;
   buffer_tx[5] = data;
   send_packet(buffer_tx, 5+len);
}

void sending_request(char slv, char func, char len, char data)
{
   do{
      create_packet(slv, func, len, data);
      delay_ms(10);
      while(!new_packet)
         if(Error)
            create_packet(slv, func, len, data);
   }
   while(buffer_rx[0] != slv | buffer_rx[2] != ACK | buffer_rx[3] != func);
}


void main()
{
   setup_psp(PSP_DISABLED);        // PSP birimi devre d���
   setup_timer_1(T1_DISABLED);     // T1 zamanlay�c�s� devre d���
   setup_timer_2(T2_DISABLED,0,1); // T2 zamanlay�c�s� devre d���
   setup_CCP1(CCP_OFF);            // CCP1 birimi devre d���
   setup_CCP2(CCP_OFF);            // CCP2 birimi devre d���
   
   enable_interrupts(GLOBAL);
   clear_interrupt(INT_RDA);
   enable_interrupts(INT_RDA);
   
   slv1_speed = 1;
   slv2_speed = 1;
   slv3_speed = 1;
   
   set_tris_a(0xFF);
   set_tris_d(0xFF);

   while(TRUE)
   {
      // Hangi slavelere paketler yollans�n
      if(input(slv1_pin))
         slv1 = TRUE;
      else
         slv1 = FALSE;
      if(input(slv2_pin))
         slv2 = TRUE;
      else
         slv2 = FALSE;
      if(input(slv3_pin))
         slv3 = TRUE;
      else
         slv3 = FALSE;
      // ----------------------------------
      
      // Fonksiyonlar�n butonlarla yollanmas�
      if(input(pin_D0)){ // Fonksiyon 0x01
         while(input(pin_D0));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x01,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x01,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x01,1,0);
         
      }else if(input(pin_D1)){ // Fonksiyon 0x02
         while(input(pin_D1));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x02,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x02,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x02,1,0);
         
      }else if(input(pin_D2)){ // Fonksiyon 0x03
         while(input(pin_D2));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x03,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x03,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x03,1,0);
         
      }else if(input(pin_D3)){ // Fonksiyon 0x04
         while(input(pin_D3));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x04,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x04,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x04,1,0);
         
      }else if(input(pin_D4)){ // Fonksiyon 0x05
         while(input(pin_D4));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x05,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x05,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x05,1,0);
         
      }else if(input(pin_D5)){ // Fonksiyon 0x06
         while(input(pin_D5));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x06,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x06,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x06,1,0);
         
      }else if(input(pin_D6)){ // Fonksiyon 0x07
         while(input(pin_D6));
         delay_ms(10);
         if(slv1) sending_request(SLAVE1,0x07,1,0);
         delay_ms(10);
         if(slv2) sending_request(SLAVE2,0x07,1,0);
         delay_ms(10);
         if(slv3) sending_request(SLAVE3,0x07,1,0);
         
      }else if(input(pin_D7)){ // Fonksiyon 0x08
         while(input(pin_D7));
         delay_ms(10);
         if(slv1){
            slv1_speed++;
            if(slv1_speed == 11) slv1_speed=1;
            sending_request(SLAVE1,0x08,1,slv1_speed);
         }
         delay_ms(10);
         if(slv2){
            slv2_speed++;
            if(slv2_speed == 11) slv2_speed=1;
            sending_request(SLAVE2,0x08,1,slv2_speed);
         }
         delay_ms(10);
         if(slv3){
            slv3_speed++;
            if(slv3_speed == 11) slv3_speed=1;
            sending_request(SLAVE3,0x08,1,slv3_speed);
         }
      }
      // ----------------------------------
   }

}
