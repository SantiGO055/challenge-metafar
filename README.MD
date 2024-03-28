Script para insertar datos:
USE [Challenge_Metafar]
GO


INSERT INTO [dbo].[Tarjeta]
           ([NroTarjeta]
           ,[Pin]
           ,[Intentos]
           ,[TarjetaBloqueada])
     VALUES
           (123456
           ,1234
           ,3
           ,'false')
GO



INSERT INTO [dbo].[TipoMovimiento]
           ([IDMovimiento]
           ,[DescripcionMovimiento])
     VALUES
           (1
           ,'Extraccion'), (2,'Consulta')
GO



INSERT INTO [dbo].[CuentaBancaria]
           ([IDMovimiento]
           ,[IDTarjeta]
           ,[NroCuenta]
           ,[Saldo])
     VALUES
           (1
           ,1
           ,234
           ,20000.00)
GO






INSERT INTO [dbo].[Usuario]
           ([IDCuentaBancaria]
           ,[Nombre])
     VALUES
           (1
           ,'Pepito')
GO





INSERT INTO [dbo].[Movimiento]
           ([IDCuentaBancaria]
           ,[IDTipoMovimiento]
           ,[FechaMovimiento])
     VALUES
           (1
           ,2
           ,'27/03/2024')
GO