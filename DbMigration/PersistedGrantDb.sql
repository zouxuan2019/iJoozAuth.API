
    CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
        MigrationId nvarchar(150) NOT NULL,
        ProductVersion nvarchar(32) NOT NULL,
        CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
    );


CREATE TABLE IF NOT EXISTS DeviceCodes (
    DeviceCode nvarchar(200) NOT NULL,
    UserCode nvarchar(200) NOT NULL,
    SubjectId nvarchar(200) NULL,
    ClientId nvarchar(200) NOT NULL,
    CreationTime datetime NOT NULL,
    Expiration datetime NOT NULL,
    Data nvarchar(4000) NOT NULL,
    CONSTRAINT PK_DeviceCodes PRIMARY KEY (UserCode)
);



CREATE TABLE IF NOT EXISTS PersistedGrants (
    `Key` nvarchar(200) NOT NULL,
    Type nvarchar(50) NOT NULL,
    SubjectId nvarchar(200) NULL,
    ClientId nvarchar(200) NOT NULL,
    CreationTime datetime NOT NULL,
    Expiration datetime NULL,
    Data nvarchar(4000) NOT NULL,
    CONSTRAINT PK_PersistedGrants PRIMARY KEY (`Key`)
);


CREATE UNIQUE INDEX IX_DeviceCodes_DeviceCode ON DeviceCodes (DeviceCode);


CREATE INDEX IX_PersistedGrants_SubjectId_ClientId_Type ON PersistedGrants (SubjectId, ClientId, Type);


INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion)
VALUES (N'20181129175506_Grants', N'2.1.4-rtm-31024');

