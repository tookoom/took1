/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [SiteAdID]
      ,[AdTypeID]
      ,'pieta'
      ,c.Name
      ,st.Name
      ,s.CityName
      ,s.DistrictName
      ,s.TotalArea
      ,s.TotalRooms
      ,s.InternalArea
      ,s.ExternalArea
      ,[Price]
      ,sitead.IPTU
      ,sitead.Cond
      ,''
      ,sitead.ShortDescription
      ,sitead.Description 
      ,sitead.AreaDescription
      ,sitead.CondDescription
      ,''

  FROM [pietaimoveis].[dbo].[SiteAd] sitead
	join dbo.Category c on sitead.CategoryID = c.CategoryID
	join dbo.Site s on sitead.SiteID = s.SiteID
	join SiteType st on s.SiteTypeID = st.SiteTypeID
  where AdTypeID = 1