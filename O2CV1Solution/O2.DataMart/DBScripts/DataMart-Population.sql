/* this script populates [DataMart] using data from [Mortgages].[Input_Example] */

alter table  [dbo].[Input_Example] drop column PropertyId
go
alter table [dbo].[Input_Example] add PropertyId uniqueidentifier
go
alter table  [dbo].[Input_Example] drop column Owner1Id
go
alter table [dbo].[Input_Example] add Owner1Id uniqueidentifier
go
alter table  [dbo].[Input_Example] drop column Owner2Id
go
alter table [dbo].[Input_Example] add Owner2Id uniqueidentifier
go
alter table  [dbo].[Input_Example] drop column Loan1Id
go
alter table [dbo].[Input_Example] add Loan1Id uniqueidentifier
go
alter table  [dbo].[Input_Example] drop column Loan2Id
go
alter table [dbo].[Input_Example] add Loan2Id uniqueidentifier
go


update [dbo].[Input_Example] set
PropertyId = newid(),
Owner1Id = newid(),
Owner2Id = newid(),
Loan1Id = newid(),
Loan2Id = newid()
go

insert 
[DataMart].[dbo].[Persons]
select
Owner1Id,
[Owner Full Name],
[Owner First Name 1],
[Owner Last Name 1],
[Mailing Full Address],
[Mailing House Number],
[Mailing Directional],
[Mailing Street Name],
[Mailing Apartment   Unit Number],
[Mailing City],
[Mailing State],
[Mailing Zip Code +4],
[Mailing Zip Code +4],
[Mailing Carrier Route],
[HOME VALUE],
null
from
[Mortgages].[dbo].[Input_Example]
where datalength([Owner First Name 1]) > 0
go

insert 
[DataMart].[dbo].[Persons]
select
Owner2Id,
[Owner Full Name],
[Owner First Name 2],
[Owner Last Name 2],
[Mailing Full Address],
[Mailing House Number],
[Mailing Directional],
[Mailing Street Name],
[Mailing Apartment   Unit Number],
[Mailing City],
[Mailing State],
[Mailing Zip Code +4],
[Mailing Zip Code +4],
[Mailing Carrier Route],
[HOME VALUE],
null
from
[Mortgages].[dbo].[Input_Example]
where datalength([Owner First Name 2]) > 0
go

insert 
[DataMart].[dbo].[Property]
select
PropertyId,
[FIPS],
[APN],
[County],
[Property Land Use Code Description],
case [Owner Occupancy Indicator] when 'y' then 1 when 'n' then 0 else [Owner Occupancy Indicator] end,
[Property Full Address],
[Property House Number],
[Property Street Direction],
[Property Street Name],
[Property Apartment   Unit Number],
[Property City],
[Property State],
[Property Zip Code +4],
[Property Zip Code +4],
[Property Carrier Route Code],
[Sale Date],
[Sale Recording Date],
[Sale Price],
[HOME VALUE],
null,
[CLTV]
from
[Mortgages].[dbo].[Input_Example]
go


insert 
[DataMart].[dbo].[Mortgages]
select
Loan1Id,
[First Mortgage Lender Name],
[First Mortgage Loan Amount],
null,
[First Mortgage Origination Date],
[First Mortgage Recording Date],
[First Mortgage Maturity Date],
[First Mortgage Mortgage Term],
[First Mortgage Type],
[First Mortgage Mortgage Type],
[First Mortgage Interest Rate],
[First Mortgage Interest Rate Type]
from
[Mortgages].[dbo].[Input_Example]
where datalength([First Mortgage Lender Name]) > 0
go


insert 
[DataMart].[dbo].[Mortgages]
select
Loan2Id,
[Junior Mortgage Lender Name],
[Junior Mortgage Mortgage Amount],
null,
[Junior Mortgage Origination Date],
[Junior Mortgage Recording Date],
[Junior Mortgage Maturity Date],
[Junior Mortgage Mortgage Term],
[Junior Mortgage Type],
[Junior Mortgage Mortgage Type],
[Junior Mortgage Interest Rate],
[Junior Mortgage Rate Type]
from
[Mortgages].[dbo].[Input_Example]
where datalength([Junior Mortgage Lender Name]) > 0
go


insert [DataMart].dbo.backbone 
select  newid(), PropertyId, Owner1Id, 1, Loan1Id, 1
from [Mortgages].[dbo].[Input_Example]
go
insert [DataMart].dbo.backbone 
select newid(), PropertyId, Owner2Id, 2, Loan1Id, 1
from [Mortgages].[dbo].[Input_Example]
where datalength([Owner First Name 2]) > 0
go
insert [DataMart].dbo.backbone 
select newid(), PropertyId, Owner1Id, 1, Loan2Id, 2
from [Mortgages].[dbo].[Input_Example]
where datalength([Junior Mortgage Lender Name]) > 0
go
insert [DataMart].dbo.backbone 
select newid(), PropertyId, Owner2Id, 2, Loan2Id, 2
from [Mortgages].[dbo].[Input_Example]
where  datalength([Owner First Name 2]) > 0 and datalength([Junior Mortgage Lender Name]) > 0
go


