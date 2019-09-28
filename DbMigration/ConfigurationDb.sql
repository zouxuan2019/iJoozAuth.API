
CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
    MigrationId nvarchar(150) NOT NULL,
    ProductVersion nvarchar(32) NOT NULL,
    CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
    );




CREATE TABLE IF NOT EXISTS ApiResources (
    Id int NOT NULL AUTO_INCREMENT,
    Enabled bit NOT NULL,
    Name nvarchar(200) NOT NULL,
    DisplayName nvarchar(200) NULL,
    Description nvarchar(1000) NULL,
    Created datetime  NULL,
    Updated datetime NULL,
    LastAccessed datetime NULL,
    NonEditable bit  NULL,
    CONSTRAINT PK_ApiResources PRIMARY KEY (Id)
    );



CREATE TABLE IF NOT EXISTS Clients (
    Id int NOT NULL AUTO_INCREMENT,
    Enabled bit NOT NULL,
    ClientId nvarchar(200) NOT NULL,
    ProtocolType nvarchar(200) NOT NULL,
    RequireClientSecret bit NOT NULL,
    ClientName nvarchar(200) NULL,
    Description nvarchar(1000) NULL,
    ClientUri nvarchar(2000) NULL,
    LogoUri nvarchar(2000) NULL,
    RequireConsent bit NOT NULL,
    AllowRememberConsent bit NOT NULL,
    AlwaysIncludeUserClaimsInIdToken bit NOT NULL,
    RequirePkce bit NOT NULL,
    AllowPlainTextPkce bit NOT NULL,
    AllowAccessTokensViaBrowser bit NOT NULL,
    FrontChannelLogoutUri nvarchar(2000) NULL,
    FrontChannelLogoutSessionRequired bit NOT NULL,
    BackChannelLogoutUri nvarchar(2000) NULL,
    BackChannelLogoutSessionRequired bit NOT NULL,
    AllowOfflineAccess bit  NULL,
    IdentityTokenLifetime int  NULL,
    AccessTokenLifetime int  NULL,
    AuthorizationCodeLifetime int  NULL,
    ConsentLifetime int NULL,
    AbsoluteRefreshTokenLifetime int  NULL,
    SlidingRefreshTokenLifetime int  NULL,
    RefreshTokenUsage int  NULL,
    UpdateAccessTokenClaimsOnRefresh bit  NULL,
    RefreshTokenExpiration int  NULL,
    AccessTokenType int  NULL,
    EnableLocalLogin bit  NULL,
    IncludeJwtId bit  NULL,
    AlwaysSendClientClaims bit NOT NULL,
    ClientClaimsPrefix nvarchar(200) NULL,
    PairWiseSubjectSalt nvarchar(200) NULL,
    Created datetime  NULL,
    Updated datetime NULL,
    LastAccessed datetime NULL,
    UserSsoLifetime int NULL,
    UserCodeType nvarchar(100) NULL,
    DeviceCodeLifetime int NULL,
    NonEditable bit  NULL,
    CONSTRAINT PK_Clients PRIMARY KEY (Id)
    );



CREATE TABLE IF NOT EXISTS IdentityResources (
    Id int NOT NULL AUTO_INCREMENT,
    Enabled bit NOT NULL,
    Name nvarchar(200) NOT NULL,
    DisplayName nvarchar(200) NULL,
    Description nvarchar(1000) NULL,
    Required bit NOT NULL,
    Emphasize bit NOT NULL,
    ShowInDiscoveryDocument bit NOT NULL,
    Created datetime  NULL,
    Updated datetime NULL,
    NonEditable bit NULL,
    CONSTRAINT PK_IdentityResources PRIMARY KEY (Id)
    );



CREATE TABLE IF NOT EXISTS ApiClaims (
    Id int NOT NULL AUTO_INCREMENT,
    Type nvarchar(200) NOT NULL,
    ApiResourceId int NOT NULL,
    CONSTRAINT PK_ApiClaims PRIMARY KEY (Id),
    CONSTRAINT FK_ApiClaims_ApiResources_ApiResourceId FOREIGN KEY (ApiResourceId) REFERENCES ApiResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ApiProperties (
    Id int NOT NULL AUTO_INCREMENT,
    `Key` nvarchar(250) NOT NULL,
    Value nvarchar(2000) NOT NULL,
    ApiResourceId int NOT NULL,
    CONSTRAINT PK_ApiProperties PRIMARY KEY (Id),
    CONSTRAINT FK_ApiProperties_ApiResources_ApiResourceId FOREIGN KEY (ApiResourceId) REFERENCES ApiResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ApiScopes (
    Id int NOT NULL AUTO_INCREMENT,
    Name nvarchar(200) NOT NULL,
    DisplayName nvarchar(200) NULL,
    Description nvarchar(1000) NULL,
    Required bit NOT NULL,
    Emphasize bit NOT NULL,
    ShowInDiscoveryDocument bit NOT NULL,
    ApiResourceId int NOT NULL,
    CONSTRAINT PK_ApiScopes PRIMARY KEY (Id),
    CONSTRAINT FK_ApiScopes_ApiResources_ApiResourceId FOREIGN KEY (ApiResourceId) REFERENCES ApiResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ApiSecrets (
    Id int NOT NULL AUTO_INCREMENT,
    Description nvarchar(1000) NULL,
    Value nvarchar(4000) NOT NULL,
    Expiration datetime NULL,
    Type nvarchar(250) NOT NULL,
    Created datetime  NULL,
    ApiResourceId int NOT NULL,
    CONSTRAINT PK_ApiSecrets PRIMARY KEY (Id),
    CONSTRAINT FK_ApiSecrets_ApiResources_ApiResourceId FOREIGN KEY (ApiResourceId) REFERENCES ApiResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientClaims (
    Id int NOT NULL AUTO_INCREMENT,
    Type nvarchar(250) NOT NULL,
    Value nvarchar(250) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientClaims PRIMARY KEY (Id),
    CONSTRAINT FK_ClientClaims_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientCorsOrigins (
    Id int NOT NULL AUTO_INCREMENT,
    Origin nvarchar(150) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientCorsOrigins PRIMARY KEY (Id),
    CONSTRAINT FK_ClientCorsOrigins_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientGrantTypes (
    Id int NOT NULL AUTO_INCREMENT,
    GrantType nvarchar(250) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientGrantTypes PRIMARY KEY (Id),
    CONSTRAINT FK_ClientGrantTypes_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientIdPRestrictions (
    Id int NOT NULL AUTO_INCREMENT,
    Provider nvarchar(200) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientIdPRestrictions PRIMARY KEY (Id),
    CONSTRAINT FK_ClientIdPRestrictions_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientPostLogoutRedirectUris (
    Id int NOT NULL AUTO_INCREMENT,
    PostLogoutRedirectUri nvarchar(2000) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientPostLogoutRedirectUris PRIMARY KEY (Id),
    CONSTRAINT FK_ClientPostLogoutRedirectUris_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientProperties (
    Id int NOT NULL AUTO_INCREMENT,
    `Key` nvarchar(250) NOT NULL,
    Value nvarchar(2000) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientProperties PRIMARY KEY (Id),
    CONSTRAINT FK_ClientProperties_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientRedirectUris (
    Id int NOT NULL AUTO_INCREMENT,
    RedirectUri nvarchar(2000) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientRedirectUris PRIMARY KEY (Id),
    CONSTRAINT FK_ClientRedirectUris_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientScopes (
    Id int NOT NULL AUTO_INCREMENT,
    Scope nvarchar(200) NOT NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientScopes PRIMARY KEY (Id),
    CONSTRAINT FK_ClientScopes_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ClientSecrets (
    Id int NOT NULL AUTO_INCREMENT,
    Description nvarchar(2000) NULL,
    Value nvarchar(4000) NOT NULL,
    Expiration datetime NULL,
    Type nvarchar(250) NOT NULL,
    Created datetime  NULL,
    ClientId int NOT NULL,
    CONSTRAINT PK_ClientSecrets PRIMARY KEY (Id),
    CONSTRAINT FK_ClientSecrets_Clients_ClientId FOREIGN KEY (ClientId) REFERENCES Clients (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS IdentityClaims (
    Id int NOT NULL AUTO_INCREMENT,
    Type nvarchar(200) NOT NULL,
    IdentityResourceId int NOT NULL,
    CONSTRAINT PK_IdentityClaims PRIMARY KEY (Id),
    CONSTRAINT FK_IdentityClaims_IdentityResources_IdentityResourceId FOREIGN KEY (IdentityResourceId) REFERENCES IdentityResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS IdentityProperties (
    Id int NOT NULL AUTO_INCREMENT,
    `Key` nvarchar(250) NOT NULL,
    Value nvarchar(2000) NOT NULL,
    IdentityResourceId int NOT NULL,
    CONSTRAINT PK_IdentityProperties PRIMARY KEY (Id),
    CONSTRAINT FK_IdentityProperties_IdentityResources_IdentityResourceId FOREIGN KEY (IdentityResourceId) REFERENCES IdentityResources (Id) ON DELETE CASCADE
    );



CREATE TABLE IF NOT EXISTS ApiScopeClaims (
    Id int NOT NULL AUTO_INCREMENT,
    Type nvarchar(200) NOT NULL,
    ApiScopeId int NOT NULL,
    CONSTRAINT PK_ApiScopeClaims PRIMARY KEY (Id),
    CONSTRAINT FK_ApiScopeClaims_ApiScopes_ApiScopeId FOREIGN KEY (ApiScopeId) REFERENCES ApiScopes (Id) ON DELETE CASCADE
    );



CREATE INDEX IX_ApiClaims_ApiResourceId ON ApiClaims (ApiResourceId);



CREATE INDEX IX_ApiProperties_ApiResourceId ON ApiProperties (ApiResourceId);



CREATE UNIQUE INDEX IX_ApiResources_Name ON ApiResources (Name);



CREATE INDEX IX_ApiScopeClaims_ApiScopeId ON ApiScopeClaims (ApiScopeId);



CREATE INDEX IX_ApiScopes_ApiResourceId ON ApiScopes (ApiResourceId);



CREATE UNIQUE INDEX IX_ApiScopes_Name ON ApiScopes (Name);



CREATE INDEX IX_ApiSecrets_ApiResourceId ON ApiSecrets (ApiResourceId);



CREATE INDEX IX_ClientClaims_ClientId ON ClientClaims (ClientId);



CREATE INDEX IX_ClientCorsOrigins_ClientId ON ClientCorsOrigins (ClientId);



CREATE INDEX IX_ClientGrantTypes_ClientId ON ClientGrantTypes (ClientId);



CREATE INDEX IX_ClientIdPRestrictions_ClientId ON ClientIdPRestrictions (ClientId);



CREATE INDEX IX_ClientPostLogoutRedirectUris_ClientId ON ClientPostLogoutRedirectUris (ClientId);



CREATE INDEX IX_ClientProperties_ClientId ON ClientProperties (ClientId);



CREATE INDEX IX_ClientRedirectUris_ClientId ON ClientRedirectUris (ClientId);



CREATE UNIQUE INDEX IX_Clients_ClientId ON Clients (ClientId);



CREATE INDEX IX_ClientScopes_ClientId ON ClientScopes (ClientId);



CREATE INDEX IX_ClientSecrets_ClientId ON ClientSecrets (ClientId);



CREATE INDEX IX_IdentityClaims_IdentityResourceId ON IdentityClaims (IdentityResourceId);



CREATE INDEX IX_IdentityProperties_IdentityResourceId ON IdentityProperties (IdentityResourceId);



CREATE UNIQUE INDEX IX_IdentityResources_Name ON IdentityResources (Name);



INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
VALUES (N'20190927175524_Config', N'2.1.4-rtm-31024');


