SELECT [UserID]
      ,[PersonID]
      ,[ContextID]
      ,[Name]
      ,[Password]
  FROM [tk1].[TK1].[User]
GO

SELECT *
  FROM [tk1].[TK1].[Person]
GO

DELETE FROM [tk1].Bizz.SiteAdDetail
DELETE FROM [tk1].Bizz.SiteAdPic
DELETE FROM [tk1].Bizz.SiteAdIDGenerator
DELETE FROM [tk1].Bizz.SiteAd
