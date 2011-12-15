USE [CandyDirectAx]
GO

/****** Object:  Table [dbo].[CandyDirectProcessedOrders]    Script Date: 12/14/2011 18:28:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CandyDirectProcessedOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[Store] [varchar](50) NULL,
	[StoreEntityId] [varbinary](50) NULL,
	[OrderNumber] [varbinary](100) NULL,
 CONSTRAINT [PK_CandyDirectProcessedOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


