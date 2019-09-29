
# Generate self signed certificate
—1.Create the certificate and private key
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout ijooz.key -out ijooz.crt -subj "/CN=ijooz.com" -days 3650

—2.Convert the certificate and key into a self-contained .pfx file

openssl pkcs12 -export -out ijooz.pfx -inkey ijooz.key -in ijooz.crt -certfile ijooz.crt

-3.Set ijooz.cert as signing key in resource API which need to be protected by jwt token


##Deploy to google cloud

Refer to https://codelabs.developers.google.com/codelabs/cloud-app-engine-aspnetcore/#1

0. Install gcloud cli
1. dotnet publish -c Release
2. cd to publish folder, copy app.yaml to this folder
3. gcloud app deploy


#Refer to test.http to call different endpoint
