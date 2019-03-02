import binascii
import sys

file_name = sys.argv[1]
with open (file_name) as f:
	hexdata = binascii.hexlify(f.read())
	hexlist = map(''.join, zip(hexdata[::2], hexdata[1::2]))
	shellcode = ''
	for i in hexlist:
		shellcode += "0x{},".format(i)
	shellcode = shellcode[:-1]
	output = open('shellcode.txt', 'w')
	output.write(shellcode)
	output.close()

print "Shellcode written to shellcode.txt"