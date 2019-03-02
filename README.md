# CPLResourceRunner

Create Payload "RAW" fully-staged (S) (x86) in cobalt strike

Run ConvertShellcode.py on your beacon.bin file

Run the following command against the "shellcode.txt" file to get a blob for the cpl resource.

cat shellcode.txt |sed 's/[, ]//g; s/0x//g;' |tr -d '\n' |xxd -p -r |gzip -c |base64 > b64shellcode.txt

Copy b64shellcode.txt contents into Resources.txt in this project.

Compile to x86 and copy CPLResourceRunner.dll to RunMe.cpl

Will launch with double click or whatever method you use to execute files.

For asthetics, change the contents of the MsgBox to suit your pretext or remove for lateral movement usage.
