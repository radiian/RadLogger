#include "radlogger.h"

int main(){
	std::string requestdata = "POST /write?db=testdb HTTP/1.1\r\nHost:10.0.0.118:8086\r\nContent-Length: 19\r\nContent-Type: x-www-form-urlencoded\r\n\r\ntestpoint value=12";
	std::cout << "RadLogger\n";

	Sock testsock = Sock(tcp, "10.0.0.118", 8086);
	std::string message = "";
	testsock.Connect();	


	testsock.Write(requestdata);
	std::cout << "Server said: " << testsock.Read() << std::endl;

	/*while(message != "exit"){
		std::getline(std::cin, message);
		std::cout << "Message: " << message << std::endl;
		testsock.Write(message + "\r\n");
		std::cout << "Server said: " << testsock.Read() << std::endl;
	}*/
}
