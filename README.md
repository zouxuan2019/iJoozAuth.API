
—1.Create the certificate and private key
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout ijooz.key -out ijooz.crt -subj "/CN=ijooz.com" -days 3650

—2.Convert the certificate and key into a self-contained .pfx file

openssl pkcs12 -export -out ijooz.pfx -inkey ijooz.key -in ijooz.crt -certfile ijooz.crt

-3.Copy ijooz.cert to resource API which need to be protected by jwt token
