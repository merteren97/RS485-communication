/*
Paketin Protokolü:

Address FROM | Address TO | Control | Function | Length | Data | CRC

Address FROM : 1 byte   (Paketin oluþturulup yollandýðý PIC adresi)
Address TO :   1 byte   (Paketin ulaþacaðý PIC adresi)
Control :      1 byte   (ACK ve NACK iþlemleri)
Funtion :      1 byte   (Fonskiyon listesinden seçilecek)
Length :       1 byte   (Data nýn uzunluðunu belirtmek için)
Data :         0-N byte (Ýstendiði kadar uzatýlabilinen data)
CRC :          1 byte   (8 bit CRC)

Komut Listesi:

0x01- Motor ->Tam Tur Sað
0x02- Motor ->Tam Tur Sol
0x03- Motor ->Yarým Tur Sað
0x04- Motor ->Yarým Tur Sol
0x05- Motor ->Toggle Sað
0x06- Motor ->Toggle Sol
0x07- Motor ->Motoru durdur
0x08- Motor ->Hýz deðerini yazma
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

int buffer_tx[BUFFER_SIZE];
int buffer_rx[BUFFER_SIZE];
char i,j,k,num;
char P_Length, Error, new_packet;

char step[8]={0b00000001,0b00000011,0b00000010,0b00000110,0b000000100,0b00001100,0b00001000,0b00001001};
char speed=10;
char toggle;

#int_RDA
void iletisim()
{
   new_packet=FALSE;
   buffer_rx[0] = getc(); // kimden
   buffer_rx[1] = getc(); // hedefimiz
   if(buffer_rx[1] == SLAVE3)
   {
      buffer_rx[2] = getc(); // ack
      buffer_rx[3] = getc(); // fonksiyon
      buffer_rx[4] = getc(); // datanýn uzunluðu
      
      for(k=0; k<(buffer_rx[4]+1); k++) //data larýn alýnmasý
         buffer_rx[k+5] = getc();
         
      P_Length = 6+buffer_rx[4]; // Paketin uzunluðu

      Error = FALSE;
      if(buffer_rx[k+5] != generate_8bit_crc(buffer_rx, P_Length, CRC_KEY)) // CRC kontrolü
         Error = TRUE;
      
      if(buffer_rx[1] == SLAVE3 && Error == FALSE)
         new_packet = TRUE;
         
      if(buffer_rx[3] == 0x07 && Error == FALSE) // Eðer fonksiyon 0x07 gelirse motor durdur.
         toggle = FALSE;
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
   buffer_tx[0] = slv;
   buffer_tx[1] = MASTER;
   buffer_tx[2] = ACK;
   buffer_tx[3] = func;
   buffer_tx[4] = len;
   buffer_tx[5] = data;
   send_packet(buffer_tx, 5+len);
}

void tam_tur_sag()
{
   for(i=0; i<6; i++)
   {
      for(j=0; j<8; j++)
      {
         delay_ms(speed);
         num++;
         if(num == 8)
            num=0;
         output_d(step[num]);
      }
   }   
}

void tam_tur_sol()
{
   for(i=0; i<6; i++)
   {
      for(j=0; j<8; j++)
      {
         delay_ms(speed);
         num--;
         if(num==-1)
            num=7;
         output_d(step[num]);
      }
   }
}

void yarim_tur_sag()
{
   for(i=0; i<3; i++)
   {
      for(j=0; j<8; j++)
      {
         delay_ms(speed);
         num++;
         if(num == 8)
            num=0;
         output_d(step[num]);
      }
   }   
}

void yarim_tur_sol()
{
   for(i=0; i<3; i++)
   {
      for(j=0; j<8; j++)
      {
         delay_ms(speed);
         num--;
         if(num==-1)
            num=7;
         output_d(step[num]);
      }
   }
}

void toggle_sag()
{
   toggle = TRUE;
   while(toggle)
   {
      delay_ms(speed);
      num++;
      if(num == 8)
         num=0;
      output_d(step[num]);
   }
   create_packet(SLAVE3,0x07,1,0); // ACK
}

void toggle_sol()
{
   toggle = TRUE;
   while(toggle)
   {
      delay_ms(speed);
      num--;
      if(num==-1)
         num=7;
      output_d(step[num]);
   }
   create_packet(SLAVE3,0x07,1,0); // ACK
}

void main()
{
   output_low(gonder);
   enable_interrupts(GLOBAL);
   clear_interrupt(INT_RDA);
   enable_interrupts(INT_RDA);
   
   output_d(step[5]);
   num = 5;
   toggle = FALSE;
   
   while(TRUE)
   {
      output_low(gonder);
      
      if(new_packet){    
         switch(buffer_rx[3])
         {
            case 0x01:
               delay_ms(20);
               create_packet(SLAVE3,0x01,1,0);
               tam_tur_sag();
               break;
            case 0x02:
               delay_ms(20);
               create_packet(SLAVE3,0x02,1,0);
               tam_tur_sol();
               break;
            case 0x03:
               delay_ms(20);
               create_packet(SLAVE3,0x03,1,0);
               yarim_tur_sag();
               break;
            case 0x04:
               delay_ms(20);
               create_packet(SLAVE3,0x04,1,0);
               yarim_tur_sol();
               break;
            case 0x05:
               delay_ms(20);
               create_packet(SLAVE3,0x05,1,0);
               toggle_sag();
               break;
            case 0x06:
               delay_ms(20);
               create_packet(SLAVE3,0x06,1,0);
               toggle_sol();
               break;
            case 0x07:
               delay_ms(20);
               create_packet(SLAVE3,0x07,1,0);
               break;
            case 0x08:
               delay_ms(20);
               create_packet(SLAVE3,0x08,1,0);
               speed = buffer_rx[5] * 10;
               break;
         }
         new_packet = FALSE;
      }
   }

}
