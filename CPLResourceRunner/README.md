# Create Cobalt Strike Payload
Attacks -> Packages -> Windows Executable (s) -> Output => RAW (x86)


Use https://gist.githubusercontent.com/rvrsh3ll/abea05538480db9e41afa3799e5053bb/raw/74cdb762da1aa8556d04959d83900eaa2d6170d6/ConvertShellcode.py
to convert beacon.bin to usable shellcode (shellcode.txt).

#Convert shellcode.txt to base64 blob to place in resource file

cat shellcode.txt |sed 's/[, ]//g; s/0x//g;' |tr -d '\n' |xxd -p -r |gzip -c |base64 > b64shellcode.txt

Credits to https://atom0s.com/forums/viewtopic.php?t=178 for the shellcode piece
Thanks to @jnqpblc for help with this project