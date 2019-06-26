import requests, serial, time

URL = "http://10.0.0.118:8086/write?db=radlog"

SerialDevice = "COM8"

port = serial.Serial(SerialDevice, 115200)

location = "desktop"


headers = {'content-type': 'application/x-www-form-urlencoded'}

data_base = "rad_data,location=" + location + " value="



while True:
	port.write(b'<GETCPM>>')
	dat = port.read(2)
	cpm = dat[0] << 8
	cpm |= dat[1]
	print(str(cpm))
	
	payload  = data_base + str(cpm)
	r = requests.post(URL, headers=headers, data=payload)
	print(payload)
	time.sleep(1)