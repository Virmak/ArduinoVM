// DECLARATIONx
      unsigned char Registers[5] = {0, 0, 0, 0, 0}; //1. ACCUMULATOR 2. AH 3. AL 4. BH 5. BL 6.X
      unsigned char Flags[3] = {0, 0, 0}; // 1 : CARRY FLAG, 2 : DATA FLAG, 3 : OVERFLOW FLAG
      unsigned char pc = -1;
      unsigned char Program[] = {0x01,0x05,0x11,0x07,0x19,0x03,0x01,0x00}; //Program Role : load value 5 into regiter 0, load value 7 into register 1 add regiter 1 with the accumuator (0) , store the accumulator value to ram at adress 0x01
      byte running = 1;
      unsigned char ins=0;
      byte Ram[255];
      unsigned char VRam[32]; // Video memory two line 16 column screen ASCII
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  Serial.println("Hello World!");
  Ram[1] =55;
}

      
void loop() {

      
      while(running >= 1) {
          fetch();
          decode();
          execute(ins);
      }
      unsigned char crap;
      
  }
  void fetch() {
      pc++;
  }
  void decode() {
      ins = Program[pc] & 0x0f;
  }
  void execute(unsigned char instruction) {
      Serial.println("INSTRUCTION : 0x%X" + instruction );
      if (Flags[1] == 0) {
          ex_phase_1(instruction); // flag 2 = 0 executable phase 1
      }
      else {
          ex_phase_2(Program[pc]);
      }
  }
  
  void ex_phase_2(unsigned char ins) {
      switch (ins) {
          case 0xA0 :
              Serial.println("Load data to Vram\n");
              pc++;
              Registers[5] = 0;
              while (Registers[5] < 32) {
                  VRam[Registers[5]] = Program[pc];
                  Serial.println("Data : " +  String(Program[pc]) +" Transferred To VRAM\n");
                  Registers[5]++;
                  pc++;
              }
              Flags[1] = 0;
              break;
          case 0xB0 :
              Registers[5] = 32;
              while (Registers[5] > 0) {
  
              }
              break;
  
      }
  
  
  }
  
  void ex_phase_1(unsigned char instruction) {
      unsigned char firstAdress;
      unsigned char counter;
      switch (instruction) {
      case 0x00 :
           running = 0;
           Serial.println("HLT\n");
           break;
      case 0x01 :
           Registers[Program[pc] >> 4] = Program[pc+1];
           Serial.println("Value = "+ String(Program[pc+1]) +" loaded to register 0x%X" + String(Program[pc] >> 4));
           pc++;
           break;
      case 0x02 :
          Registers[Program[pc] >> 4] = Ram[Program[pc+1]];
          Serial.println("Register " + String(Program[pc] >> 4) + " loaded wih adress 0x%X" + String(Program[pc+1]));
          pc++;
          break;
      case 0x03 :
          Ram[Program[pc+1]] = Registers[Program[pc] >> 4];
          Serial.println("Register " + String(Program[pc] >> 4) + " Stored at address 0x%X" + String(Program[pc+1]));
          pc++;
          break;
      case 0x4 :
          Ram[Program[pc+1]] = Program[pc+2];
          Serial.println("Value " + String(Program[pc+2]) + " loaded at address 0x%X" + String(Program[pc+2]));
          pc+=2;
               break;
      case 0x5 :
          if (Registers[0] > Registers[Program[pc] >> 4]) {
              Flags[1] = 1;
          } else if (Registers[0] < Registers[Program[pc] >> 4]){
              Flags[1] = 2;
          } else if (Registers[0] == Registers[Program[pc] >> 4])
          {
              Flags[1] = 0;
          }
          break;
      case 0x6 :
          if (Registers[0] > Program[pc+1]) {
              Flags[1] = 1;
          } else if (Registers[0] < Program[pc+1]){
              Flags[1] = 2;
          } else if (Registers[0] == Program[pc+1])
          {
              Flags[1] = 0;
          }
          pc++;
          break;
      case 0x7 :
          pc = Program[pc+1];
          break;
      case 0x8 :
          if ((Program[pc] >> 4) == Flags[1]) {
              pc = Program[pc+1];
          } else {
              pc+=2;
          }
          break;
      case 0x9 :
          Registers[0] = Registers[0] + Registers[Program[pc] >> 4];
          break;
      case 0xa :
          Registers[0] = Registers[0] - Registers[Program[pc] >> 4];
          break;
      case 0xb :
          Registers[0] = Registers[0] + Registers[Program[pc] >> 4];
          if (Registers[0] < Registers[Program[pc] >> 4]) {
              Flags[0] = 1;
          }
          break;
      case 0xc :
          Registers[0] = Registers[0] - Registers[Program[pc] >> 4];
          if (Registers[0] > Registers[Program[pc] >> 4]) {
              Flags[2] = 1;
          }
          break;
      case 0xd :
          Registers[Program[pc+1] & 0xf] = Registers[Program[pc+1] >>4];
          pc++;
          break;
      case 0xe :
          Flags[1] = Program[pc] >> 4;
          break;
      case 0xf :
          firstAdress = Program[pc+1];
          counter = 0;
          pc+=1;
          while (Registers[5] > 0) {
              pc++;
              Registers[5]--;
              Ram[counter + firstAdress] = Program[pc];
              counter++;
          }
          break;
      }
  
  }

