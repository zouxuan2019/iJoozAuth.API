
# Generate self signed certificate
—1.Create the certificate and private key
openssl req -x509 -newkey rsa:4096 -sha256 -nodes -keyout ijooz.key -out ijooz.crt -subj "/CN=ijooz.com" -days 3650

—2.Convert the certificate and key into a self-contained .pfx file

openssl pkcs12 -export -out ijooz.pfx -inkey ijooz.key -in ijooz.crt -certfile ijooz.crt

-3.Set ijooz.cert as signing key in resource API which need to be protected by jwt token


##Deploy to google cloud

Refer to https://codelabs.developers.google.com/codelabs/cloud-app-engine-aspnetcore/#1

0.Register your application for Cloud SQL Admin API in Google Cloud Platform
https://console.cloud.google.com/flows/enableapi?apiid=sqladmin&redirect=https:%2F%2Fconsole.cloud.google.com&_ga=2.244852229.-1628980075.1568042064&_gac=1.216462692.1569770225.Cj0KCQjwrMHsBRCIARIsAFgSeI0nvZjimGuKWdqhSpTUtXBFyzfCKZV3ObOk-r0GqVcIRGWRBbka0LMaAge9EALw_wcB

0. Install gcloud cli
1. dotnet publish -c Release
2. cd to publish folder, copy app.yaml to this folder
3. gcloud config set project fvmembership-auth-257211
4. gcloud beta app deploy


#Refer to test.http to call different endpoint

## Connecting to Cloud SQL from App Engine
https://cloud.google.com/sql/docs/mysql/connect-app-engine
