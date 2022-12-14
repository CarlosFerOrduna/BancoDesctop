USE [master]
GO
/****** Object:  Database [Banco]    Script Date: 15/10/2022 23:55:53 ******/
CREATE DATABASE [Banco]
    CONTAINMENT = NONE
    ON PRIMARY
    ( NAME = N'Banco', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Banco.mdf' , SIZE = 8192 KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536 KB )
    LOG ON
    ( NAME = N'Banco_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Banco_log.ldf' , SIZE = 8192 KB , MAXSIZE = 2048 GB , FILEGROWTH = 65536 KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Banco] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [Banco].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO
ALTER DATABASE [Banco] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Banco] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Banco] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Banco] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Banco] SET ARITHABORT OFF
GO
ALTER DATABASE [Banco] SET AUTO_CLOSE ON
GO
ALTER DATABASE [Banco] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Banco] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Banco] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Banco] SET CURSOR_DEFAULT GLOBAL
GO
ALTER DATABASE [Banco] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Banco] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Banco] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Banco] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Banco] SET ENABLE_BROKER
GO
ALTER DATABASE [Banco] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Banco] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Banco] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Banco] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Banco] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Banco] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Banco] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Banco] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Banco] SET MULTI_USER
GO
ALTER DATABASE [Banco] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Banco] SET DB_CHAINING OFF
GO
ALTER DATABASE [Banco] SET FILESTREAM ( NON_TRANSACTED_ACCESS = OFF )
GO
ALTER DATABASE [Banco] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO
ALTER DATABASE [Banco] SET DELAYED_DURABILITY = DISABLED
GO
ALTER DATABASE [Banco] SET ACCELERATED_DATABASE_RECOVERY = OFF
GO
ALTER DATABASE [Banco] SET QUERY_STORE = OFF
GO
USE [Banco]
GO
/****** Object:  Table [dbo].[caja_de_ahorro]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[caja_de_ahorro]
(
    [id_caja_de_ahorro] [int] IDENTITY (1,1) NOT NULL,
    [cbu]               [int]                NOT NULL,
    [saldo]             [float]              NOT NULL,
    CONSTRAINT [PK__caja_de___C71E2476D87E8EC4] PRIMARY KEY CLUSTERED
        (
         [id_caja_de_ahorro] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movimiento]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimiento]
(
    [id_movimiento]     [int] IDENTITY (1,1) NOT NULL,
    [detalle]           [varchar](50)        NOT NULL,
    [monto]             [float]              NOT NULL,
    [fecha]             [date]               NOT NULL,
    [id_caja_de_ahorro] [int]                NOT NULL,
    CONSTRAINT [PK__movimien__2A071C244A8B4BD8] PRIMARY KEY CLUSTERED
        (
         [id_movimiento] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pago]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pago]
(
    [id_pago]    [int] IDENTITY (1,1) NOT NULL,
    [detalle]    [varchar](50)        NOT NULL,
    [monto]      [float]              NOT NULL,
    [pagado]     [bit]                NOT NULL,
    [metodo]     [varchar](30)        NOT NULL,
    [id_usuario] [int]                NOT NULL,
    CONSTRAINT [PK__pago__0941B074D6CE3D39] PRIMARY KEY CLUSTERED
        (
         [id_pago] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[plazo_fijo]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[plazo_fijo]
(
    [id_plazo_fijo] [int] IDENTITY (1,1) NOT NULL,
    [monto]         [float]              NOT NULL,
    [fecha_inicio]  [datetime]           NOT NULL,
    [fecha_fin]     [datetime]           NOT NULL,
    [tasa]          [float]              NOT NULL,
    [pagado]        [bit]                NOT NULL,
    [id_usuario]    [int]                NULL,
    CONSTRAINT [PK__plazo_fi__C5236EB2AE8039B4] PRIMARY KEY CLUSTERED
        (
         [id_plazo_fijo] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tarjeta_de_credito]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tarjeta_de_credito]
(
    [id_tarjeta] [int] IDENTITY (1,1) NOT NULL,
    [numero]     [int]                NOT NULL,
    [codigoV]    [smallint]           NOT NULL,
    [limite]     [float]              NOT NULL,
    [consumos]   [float]              NOT NULL,
    [id_usuario] [int]                NOT NULL,
    CONSTRAINT [PK__tarjeta___E92BCFEA6F4D34D8] PRIMARY KEY CLUSTERED
        (
         [id_tarjeta] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario]
(
    [id_usuario]   [int] IDENTITY (1,1) NOT NULL,
    [dni]          [int]                NOT NULL,
    [nombre]       [varchar](50)        NOT NULL,
    [apellido]     [varchar](50)        NOT NULL,
    [email]        [varchar](50)        NOT NULL,
    [clave]        [varchar](50)        NOT NULL,
    [is_admin]     [bit]                NOT NULL,
    [is_bloqueado] [bit]                NOT NULL,
    CONSTRAINT [PK__usuario__4E3E04AD75A14D26] PRIMARY KEY CLUSTERED
        (
         [id_usuario] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario_caja_de_ahorro]    Script Date: 15/10/2022 23:55:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario_caja_de_ahorro]
(
    [id_usuario_caja_de_ahorro] [int] IDENTITY (1,1) NOT NULL,
    [id_usuario]                [int]                NOT NULL,
    [id_caja_de_ahorro]         [int]                NULL,
    CONSTRAINT [PK_usuario_caja_de_ahorro] PRIMARY KEY CLUSTERED
        (
         [id_usuario_caja_de_ahorro] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[plazo_fijo]
    ADD CONSTRAINT [DF_plazo_fijo_monto] DEFAULT ((0)) FOR [monto]
GO
ALTER TABLE [dbo].[movimiento]
    WITH CHECK ADD CONSTRAINT [FK_movimiento_caja_de_ahorro] FOREIGN KEY ([id_caja_de_ahorro])
        REFERENCES [dbo].[caja_de_ahorro] ([id_caja_de_ahorro])
GO
ALTER TABLE [dbo].[movimiento]
    CHECK CONSTRAINT [FK_movimiento_caja_de_ahorro]
GO
ALTER TABLE [dbo].[pago]
    WITH CHECK ADD CONSTRAINT [FK_pago_usuario] FOREIGN KEY ([id_usuario])
        REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[pago]
    CHECK CONSTRAINT [FK_pago_usuario]
GO
ALTER TABLE [dbo].[plazo_fijo]
    WITH CHECK ADD CONSTRAINT [FK_plazo_fijo_usuario] FOREIGN KEY ([id_usuario])
        REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[plazo_fijo]
    CHECK CONSTRAINT [FK_plazo_fijo_usuario]
GO
ALTER TABLE [dbo].[tarjeta_de_credito]
    WITH CHECK ADD CONSTRAINT [FK_tarjeta_de_credito_usuario] FOREIGN KEY ([id_usuario])
        REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tarjeta_de_credito]
    CHECK CONSTRAINT [FK_tarjeta_de_credito_usuario]
GO
ALTER TABLE [dbo].[usuario_caja_de_ahorro]
    WITH CHECK ADD CONSTRAINT [FK_usuario_caja_de_ahorro_caja_de_ahorro] FOREIGN KEY ([id_caja_de_ahorro])
        REFERENCES [dbo].[caja_de_ahorro] ([id_caja_de_ahorro])
GO
ALTER TABLE [dbo].[usuario_caja_de_ahorro]
    CHECK CONSTRAINT [FK_usuario_caja_de_ahorro_caja_de_ahorro]
GO
ALTER TABLE [dbo].[usuario_caja_de_ahorro]
    WITH CHECK ADD CONSTRAINT [FK_usuario_caja_de_ahorro_usuario] FOREIGN KEY ([id_usuario])
        REFERENCES [dbo].[usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[usuario_caja_de_ahorro]
    CHECK CONSTRAINT [FK_usuario_caja_de_ahorro_usuario]
GO
USE [master]
GO
ALTER DATABASE [Banco] SET READ_WRITE
GO

use Banco;
insert into usuario (dni, nombre, apellido, email, clave, is_admin, is_bloqueado)
values (123, 'admin', 'admin', 'admin@admin.com', '123', 1, 0);

use Banco;
go
create procedure transferir(
    @p_id_caja_origen int,
    @p_id_caja_destino int,
    @p_monto float
)
as
begin
    update caja_de_ahorro
    set saldo = saldo - @p_monto
    where id_caja_de_ahorro = @p_id_caja_origen;

    update caja_de_ahorro
    set saldo = saldo + @p_monto
    where id_caja_de_ahorro = @p_id_caja_destino;
end
go
create procedure pagar_tarjeta(
    @p_id_caja int,
    @p_id_tarjeta int,
    @p_monto float
)
as
begin

    UPDATE caja_de_ahorro
    SET saldo = saldo - @p_monto
    WHERE id_caja_de_ahorro = @p_id_caja;

    UPDATE tarjeta_de_credito
    SET consumos = 0
    WHERE id_tarjeta = @p_id_tarjeta;
end
go
create procedure pagar_pago_con_tarjeta(
    @p_id_tarjeta int,
    @p_id_pago int,
    @p_monto float
)
as
begin
    update tarjeta_de_credito
    set consumos = consumos + @p_monto
    where id_tarjeta = @p_id_tarjeta;

    update pago
    set pagado = 1
    where id_pago = @p_id_pago;
end
go
create procedure pagar_pago_con_caja_de_ahorro(
    @p_id_caja_de_ahorro int,
    @p_id_pago int,
    @p_monto float
)
as
begin
    update caja_de_ahorro
    set saldo = saldo - @p_monto
    where id_caja_de_ahorro = @p_id_caja_de_ahorro;

    update pago
    set pagado = 1
    where id_pago = @p_id_pago;
end
go
