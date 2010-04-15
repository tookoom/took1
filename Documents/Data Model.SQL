CREATE TABLE Account ( 
	AccountID int identity(1,1)  NOT NULL,
	Name varchar(64),
	Description varchar(64),
	CreationTimestamp datetime
)
;

CREATE TABLE Category ( 
	CategoryID int identity(1,1)  NOT NULL,
	ContextID int,
	Name varchar(64),
	Caption varchar(64),
	Description varchar(64)
)
;

CREATE TABLE ContactInfo ( 
	ContactInfoID int identity(1,1)  NOT NULL,
	PersonID int,
	TelephoneNumber varchar(32) NOT NULL,
	Description varchar(64)
)
;

CREATE TABLE Context ( 
	ContextID int identity(1,1)  NOT NULL,
	Name varchar(32)
)
;

CREATE TABLE Event ( 
	EventID int identity(1,1)  NOT NULL,
	CategoryID int,
	Name varchar(50),
	RegistryTimestamp datetime,
	EventTimeStamp datetime
)
;

CREATE TABLE LocalisationInfo ( 
	LocalisationInfoID int identity(1,1)  NOT NULL,
	PersonID int,
	Address varchar(128) NOT NULL
)
;

CREATE TABLE MoneyCheckPoint ( 
	MoneyCheckPointID int identity(1,1)  NOT NULL,
	Name varchar(32) NOT NULL,
	Timestamp datetime NOT NULL
)
;

CREATE TABLE MoneyCheckPointAccount ( 
	MoneyCheckPointAccountID int identity(1,1)  NOT NULL,
	MoneyCheckPointID int NOT NULL,
	AccountID int NOT NULL,
	Value float DEFAULT 0 NOT NULL
)
;

CREATE TABLE MoneyTransaction ( 
	MoneyTransactionID int identity(1,1)  NOT NULL,
	Name varchar(50),
	EntryTimestamp datetime,
	Type int,
	CategoryID int
)
;

CREATE TABLE MoneyTransactionAccount ( 
	MoneyTransactionAccountID int identity(1,1)  NOT NULL,
	MoneyTransactionID int,
	AccountID int,
	Value float
)
;

CREATE TABLE MoneyTransactionEvent ( 
	MoneyTransactionEventID int identity(1,1)  NOT NULL,
	MoneyTransactionID int,
	EventID int
)
;

CREATE TABLE Person ( 
	PersonID int identity(1,1)  NOT NULL,
	FirstName varchar(64),
	LastName varchar(64),
	NickName varchar(64),
	FullName varchar(64) NOT NULL,
	BirthDate datetime
)
;

CREATE TABLE UserData ( 
	UserDataID int identity(1,1)  NOT NULL,
	Name varchar(32) NOT NULL,
	Password varchar(32) NOT NULL,
	PersonID int,
	ContextID int
)
;


ALTER TABLE Account ADD CONSTRAINT PK_Account 
	PRIMARY KEY CLUSTERED (AccountID)
;

ALTER TABLE Category ADD CONSTRAINT PK_Category 
	PRIMARY KEY CLUSTERED (CategoryID)
;

ALTER TABLE ContactInfo ADD CONSTRAINT PK_ContactInfo 
	PRIMARY KEY CLUSTERED (ContactInfoID)
;

ALTER TABLE Context ADD CONSTRAINT PK_Context 
	PRIMARY KEY CLUSTERED (ContextID)
;

ALTER TABLE Event ADD CONSTRAINT PK_Event 
	PRIMARY KEY CLUSTERED (EventID)
;

ALTER TABLE LocalisationInfo ADD CONSTRAINT PK_LocalisationInfo 
	PRIMARY KEY CLUSTERED (LocalisationInfoID)
;

ALTER TABLE MoneyCheckPoint ADD CONSTRAINT PK_MoneyCheckPoint 
	PRIMARY KEY CLUSTERED (MoneyCheckPointID)
;

ALTER TABLE MoneyCheckPointAccount ADD CONSTRAINT PK_MoneyCheckPointAccount 
	PRIMARY KEY CLUSTERED (MoneyCheckPointAccountID)
;

ALTER TABLE MoneyTransaction ADD CONSTRAINT PK_MoneyTransaction 
	PRIMARY KEY CLUSTERED (MoneyTransactionID)
;

ALTER TABLE MoneyTransactionAccount ADD CONSTRAINT PK_MoneyTransactionAccount 
	PRIMARY KEY CLUSTERED (MoneyTransactionAccountID)
;

ALTER TABLE MoneyTransactionEvent ADD CONSTRAINT PK_MoneyTransactionEvent 
	PRIMARY KEY CLUSTERED (MoneyTransactionEventID)
;

ALTER TABLE Person ADD CONSTRAINT PK_Person 
	PRIMARY KEY CLUSTERED (PersonID)
;

ALTER TABLE UserData ADD CONSTRAINT PK_UserData 
	PRIMARY KEY CLUSTERED (UserDataID)
;



ALTER TABLE Category ADD CONSTRAINT FK_Category_Context 
	FOREIGN KEY (ContextID) REFERENCES Context (ContextID)
;

ALTER TABLE ContactInfo ADD CONSTRAINT FK_ContactInfo_Person 
	FOREIGN KEY (PersonID) REFERENCES Person (PersonID)
;

ALTER TABLE Event ADD CONSTRAINT FK_Event_Category 
	FOREIGN KEY (CategoryID) REFERENCES Category (CategoryID)
;

ALTER TABLE LocalisationInfo ADD CONSTRAINT FK_LocalisationInfo_Person 
	FOREIGN KEY (PersonID) REFERENCES Person (PersonID)
;

ALTER TABLE MoneyCheckPointAccount ADD CONSTRAINT FK_MoneyCheckPointAccount_Account 
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID)
;

ALTER TABLE MoneyCheckPointAccount ADD CONSTRAINT FK_MoneyCheckPointAccount_MoneyCheckPoint 
	FOREIGN KEY (MoneyCheckPointID) REFERENCES MoneyCheckPoint (MoneyCheckPointID)
;

ALTER TABLE MoneyTransaction ADD CONSTRAINT FK_MoneyTransaction_Category 
	FOREIGN KEY (CategoryID) REFERENCES Category (CategoryID)
;

ALTER TABLE MoneyTransactionAccount ADD CONSTRAINT FK_MoneyTransactionAccount_Transaction 
	FOREIGN KEY (MoneyTransactionID) REFERENCES MoneyTransaction (MoneyTransactionID)
;

ALTER TABLE MoneyTransactionAccount ADD CONSTRAINT FK_MoneyTransactionAccount_Account 
	FOREIGN KEY (AccountID) REFERENCES Account (AccountID)
;

ALTER TABLE MoneyTransactionEvent ADD CONSTRAINT FK_MoneyTransactionEvent_Event 
	FOREIGN KEY (EventID) REFERENCES Event (EventID)
;

ALTER TABLE MoneyTransactionEvent ADD CONSTRAINT FK_MoneyTransactionEvent_Transaction 
	FOREIGN KEY (MoneyTransactionID) REFERENCES MoneyTransaction (MoneyTransactionID)
;

ALTER TABLE UserData ADD CONSTRAINT FK_UserData_Context 
	FOREIGN KEY (ContextID) REFERENCES Context (ContextID)
;

ALTER TABLE UserData ADD CONSTRAINT FK_UserData_Person 
	FOREIGN KEY (PersonID) REFERENCES Person (PersonID)
;
