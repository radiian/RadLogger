import requests, serial, time

URL = "http://10.0.0.118:8086/write?db=radlog"

SerialDevice = "/dev/ttyUSB0"

port = serial.Serial(SerialDevice, 115200)

location = "desktop"


headers = {'content-type': 'application/x-www-form-urlencoded'}

data_base = "rad_data,location=" + location + " value="



while True:
	port.write(b'<GETCPM>>')
	raw = port.read(2)
        
        #The encoding safe way to do this apparently
        cpm = int(raw[0].encode('hex'), 16) << 8
        cpm |= int(raw[1].encode('hex'), 16)
	#cpm = dat[0] << 8
	#cpm |= dat[1]
	print(str(cpm))
	
	payload  = data_base + str(cpm)
	r = requests.post(URL, headers=headers, data=payload)
	print(payload)
	time.sleep(1)
