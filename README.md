
—1.Create the certificate and private key
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout ijooz.key -out ijooz.crt -subj "/CN=ijooz.com" -days 3650

—2.Convert the certificate and key into a self-contained .pfx file

openssl pkcs12 -export -out ijooz.pfx -inkey ijooz.key -in ijooz.crt -certfile ijooz.crt

-3.Copy ijooz.cert to resource API which need to be protected by jwt token


# Get User Access Token

POST /connect/token HTTP/1.1
Host: localhost:7001
Content-Type: application/x-www-form-urlencoded
Cache-Control: no-cache
Postman-Token: ba9d249f-261a-a841-64ed-6e06134fbfe8

grant_type=password&client_id=ijoozClientId&client_secret=ijoozClientIdSecret&scope=offline_access&username=zouxuan&password=zouxuan

# Get Token with refresh token , Replace refresh_token in below request

POST /connect/token HTTP/1.1
Host: localhost:7001
Content-Type: application/x-www-form-urlencoded
Cache-Control: no-cache
Postman-Token: 6fcedea6-1787-7896-60cc-1f1b906b5689

grant_type=refresh_token&client_id=ijoozClientId&client_secret=ijoozClientIdSecret&refresh_token=1e2696524b51d3de026995246a9cd56ccf43f60ccee3aad13fa2d3a9f56cebc9


# Client Credential , currently scope support EWallet and QRCode
POST /connect/token HTTP/1.1
Host: localhost:7001
Content-Type: application/x-www-form-urlencoded
Cache-Control: no-cache
Postman-Token: 87444388-ee4a-2a98-8dab-64597fb21911

grant_type=client_credentials&client_id=thirdParty&client_secret=thirdPartySecret&scope=EWallet
