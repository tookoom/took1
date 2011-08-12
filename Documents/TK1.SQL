CREATE TABLE [AddressData] ( 
	[AddressInfoID] int identity(1,1)  NOT NULL,
	[AddressTypeID] int NOT NULL,
	[Address] varchar(128) NOT NULL,
	[Index] int DEFAULT 1 NOT NULL
)
;

CREATE TABLE [AddressType] ( 
	[AddressTypeID] int identity(1,1)  NOT NULL,
	[Name] varchar(32) NOT NULL
)
;

CREATE TABLE [App] ( 
	[AppID] int identity(1,1)  NOT NULL,
	[ContextID] int
)
;

CREATE TABLE [Category] ( 
	[CategoryID] int identity(1,1)  NOT NULL,
	[ContextID] int,
	[Name] varchar(64),
	[Caption] varchar(64),
	[Description] varchar(64),
	[CustomData] text
)
;

CREATE TABLE [ContactData] ( 
	[ContactInfoID] int identity(1,1)  NOT NULL,
	[ContactTypeID] int NOT NULL,
	[Data] text NOT NULL,
	[Description] varchar(64),
	[Index] int DEFAULT 1 NOT NULL
)
;

CREATE TABLE [ContactType] ( 
	[ContactTypeID] int identity(1,1)  NOT NULL,
	[Name] varchar(32) NOT NULL,
	[CustomData] text
)
;

CREATE TABLE [Context] ( 
	[ContextID] int identity(1,1)  NOT NULL,
	[Name] varchar(32),
	[CustomData] text
)
;

CREATE TABLE [Customer] ( 
	[CustomerID] int identity(1,1)  NOT NULL
)
;

CREATE TABLE [CustomerContactData] ( 
	[CustomerContactInfoID] int NOT NULL,
	[CustomerID] int NOT NULL,
	[ContactDataID] int NOT NULL
)
;

CREATE TABLE [CustomerPerson] ( 
	[CustomerPersonID] int identity(1,1)  NOT NULL,
	[CustomerID] int NOT NULL,
	[PersonID] int NOT NULL
)
;

CREATE TABLE [Parameter] ( 
	[ParameterID] int identity(1,1)  NOT NULL,
	[ParameterGroupID] int,
	[Name] varchar(64) NOT NULL,
	[Value] varchar(1024)
)
;

CREATE TABLE [ParameterGroup] ( 
	[ParameterGroupID] int identity(1,1)  NOT NULL,
	[Name] varchar(64) NOT NULL
)
;

CREATE TABLE [Person] ( 
	[PersonID] int identity(1,1)  NOT NULL,
	[FirstName] varchar(64),
	[LastName] varchar(64),
	[NickName] varchar(64),
	[FullName] varchar(64) NOT NULL,
	[BirthDate] datetime,
	[CustomSettings] text,
	[CustomData] text
)
;

CREATE TABLE [PersonAddressData] ( 
	[PersonAddressID] int identity(1,1)  NOT NULL,
	[PersonID] int NOT NULL,
	[AddressInfoID] int NOT NULL
)
;

CREATE TABLE [PersonContactData] ( 
	[PersonContactInfoID] int identity(1,1)  NOT NULL,
	[PersonID] int NOT NULL,
	[ContactInfoID] int NOT NULL
)
;

CREATE TABLE [Process] ( 
	[ProcessID] int identity(1,1)  NOT NULL,
	[AppID] int NOT NULL,
	[CustomerID] int NOT NULL
)
;

CREATE TABLE [User] ( 
	[UserID] int identity(1,1)  NOT NULL,
	[PersonID] int,
	[ContextID] int,
	[Name] varchar(32) NOT NULL,
	[Password] varchar(32) NOT NULL
)
;


ALTER TABLE [AddressData] ADD CONSTRAINT [PK_LocalisationInfo] 
	PRIMARY KEY CLUSTERED ([AddressInfoID])
;

ALTER TABLE [AddressType] ADD CONSTRAINT [PK_LocalisationInfoType] 
	PRIMARY KEY CLUSTERED ([AddressTypeID])
;

ALTER TABLE [App] ADD CONSTRAINT [PK_App] 
	PRIMARY KEY CLUSTERED ([AppID])
;

ALTER TABLE [Category] ADD CONSTRAINT [PK_Category] 
	PRIMARY KEY CLUSTERED ([CategoryID])
;

ALTER TABLE [ContactData] ADD CONSTRAINT [PK_ContactInfo] 
	PRIMARY KEY CLUSTERED ([ContactInfoID])
;

ALTER TABLE [ContactType] ADD CONSTRAINT [PK_ContactInfoType] 
	PRIMARY KEY CLUSTERED ([ContactTypeID])
;

ALTER TABLE [Context] ADD CONSTRAINT [PK_Context] 
	PRIMARY KEY CLUSTERED ([ContextID])
;

ALTER TABLE [Customer] ADD CONSTRAINT [PK_Customer] 
	PRIMARY KEY CLUSTERED ([CustomerID])
;

ALTER TABLE [CustomerContactData] ADD CONSTRAINT [PK_CompanyContactInfo] 
	PRIMARY KEY CLUSTERED ([CustomerContactInfoID])
;

ALTER TABLE [CustomerPerson] ADD CONSTRAINT [PK_CompanyPerson] 
	PRIMARY KEY CLUSTERED ([CustomerPersonID])
;

ALTER TABLE [Parameter] ADD CONSTRAINT [PK_Parameter] 
	PRIMARY KEY CLUSTERED ([ParameterID])
;

ALTER TABLE [ParameterGroup] ADD CONSTRAINT [PK_ParameterGroup] 
	PRIMARY KEY CLUSTERED ([ParameterGroupID])
;

ALTER TABLE [Person] ADD CONSTRAINT [PK_Person] 
	PRIMARY KEY CLUSTERED ([PersonID])
;

ALTER TABLE [PersonAddressData] ADD CONSTRAINT [PK_PersonAddress] 
	PRIMARY KEY CLUSTERED ([PersonAddressID])
;

ALTER TABLE [PersonContactData] ADD CONSTRAINT [PK_PersonContactInfo] 
	PRIMARY KEY CLUSTERED ([PersonContactInfoID])
;

ALTER TABLE [Process] ADD CONSTRAINT [PK_Process] 
	PRIMARY KEY CLUSTERED ([ProcessID])
;

ALTER TABLE [User] ADD CONSTRAINT [PK_UserData] 
	PRIMARY KEY CLUSTERED ([UserID])
;



ALTER TABLE [AddressData] ADD CONSTRAINT [FK_AddressInfo_AddressType] 
	FOREIGN KEY ([AddressTypeID]) REFERENCES [AddressType] ([AddressTypeID])
;

ALTER TABLE [App] ADD CONSTRAINT [FK_App_Context] 
	FOREIGN KEY ([ContextID]) REFERENCES [Context] ([ContextID])
;

ALTER TABLE [Category] ADD CONSTRAINT [FK_Category_Context] 
	FOREIGN KEY ([ContextID]) REFERENCES [Context] ([ContextID])
;

ALTER TABLE [ContactData] ADD CONSTRAINT [FK_ContactInfo_ContactInfoType] 
	FOREIGN KEY ([ContactTypeID]) REFERENCES [ContactType] ([ContactTypeID])
;

ALTER TABLE [CustomerContactData] ADD CONSTRAINT [FK_CompanyContactInfo_ContactInfo] 
	FOREIGN KEY ([ContactDataID]) REFERENCES [ContactData] ([ContactInfoID])
;

ALTER TABLE [CustomerContactData] ADD CONSTRAINT [FK_CustomerContactData_Customer] 
	FOREIGN KEY ([CustomerID]) REFERENCES [Customer] ([CustomerID])
;

ALTER TABLE [CustomerPerson] ADD CONSTRAINT [FK_CompanyPerson_Person] 
	FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
;

ALTER TABLE [CustomerPerson] ADD CONSTRAINT [FK_CustomerPerson_Customer] 
	FOREIGN KEY ([CustomerID]) REFERENCES [Customer] ([CustomerID])
;

ALTER TABLE [Parameter] ADD CONSTRAINT [FK_Parameter_ParameterGroup] 
	FOREIGN KEY ([ParameterGroupID]) REFERENCES [ParameterGroup] ([ParameterGroupID])
;

ALTER TABLE [PersonAddressData] ADD CONSTRAINT [FK_PersonAddress_AddressInfo] 
	FOREIGN KEY ([AddressInfoID]) REFERENCES [AddressData] ([AddressInfoID])
;

ALTER TABLE [PersonAddressData] ADD CONSTRAINT [FK_PersonAddress_Person] 
	FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
;

ALTER TABLE [PersonContactData] ADD CONSTRAINT [FK_PersonContactInfo_ContactInfo] 
	FOREIGN KEY ([ContactInfoID]) REFERENCES [ContactData] ([ContactInfoID])
;

ALTER TABLE [PersonContactData] ADD CONSTRAINT [FK_PersonContactInfo_Person] 
	FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
;

ALTER TABLE [Process] ADD CONSTRAINT [FK_Process_App] 
	FOREIGN KEY ([AppID]) REFERENCES [App] ([AppID])
;

ALTER TABLE [Process] ADD CONSTRAINT [FK_Process_Customer] 
	FOREIGN KEY ([CustomerID]) REFERENCES [Customer] ([CustomerID])
;

ALTER TABLE [User] ADD CONSTRAINT [FK_UserData_Context] 
	FOREIGN KEY ([ContextID]) REFERENCES [Context] ([ContextID])
;

ALTER TABLE [User] ADD CONSTRAINT [FK_UserData_Person] 
	FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID])
;
