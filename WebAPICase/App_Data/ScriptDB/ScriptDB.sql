USE [WebAPICase]
GO
CREATE TABLE Historico (
	idHistorico INT IDENTITY(1,1) NOT NULL,
	regiao VARCHAR(2) NOT NULL,
	estado VARCHAR(2) NOT NULL,
	municipio VARCHAR(120) NOT NULL,
	revenda VARCHAR(250) NOT NULL,
	instalacaoCodigo VARCHAR(10) NOT NULL,
	produto VARCHAR(50) NOT NULL,
	dataColeta date NOT NULL,
	valorCompra decimal(5,2) NOT NULL,
	valorVenda decimal(5,2) NULL,
	undMedida VARCHAR(15) NOT NULL,
	bandeira VARCHAR(120) NOT NULL
)








/****** Object:  Table [dbo].[Usuario]    Script Date: 12/06/2019 16:38:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](150) NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[senha] [varchar](25) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [WebAPICase] SET  READ_WRITE 
GO
