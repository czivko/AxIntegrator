USE [CandyDirectAx]
GO
 
/****** Object:  Table [dbo].[CandyDirectProcessedOrders]    Script Date: 01/18/2012 12:50:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CandyDirectAmazonOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] null,
	[Store] [varchar](50) NULL,
	[StoreEntityId] [varchar](50) NULL,
	[OrderNumber] [varchar](100) NULL,
	[StoreCreatedAt] [datetime] NULL,
	[StoreStatus] [nvarchar](50) NULL,
	[CustomerName] varchar(400) NULL,
	[ShipStreet] [nvarchar](300) NULL,
	[ShipCity] varchar(400) NULL,
	[ShipState] varchar(100) NULL,
	[ShipZip] varchar(10) NULL,
	[ShipCountry] varchar(100) NULL,
 CONSTRAINT [PK_CandyDirectAmazonOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


