update Property set County=ltrim(rtrim([county])) ,[State]=ltrim(rtrim([State])) ,City=ltrim(rtrim([City])) 
delete cities
go
insert Cities
select distinct ltrim(rtrim([city])), ltrim(rtrim([county])) from Property
go

alter table Counties drop constraint  [PK_dbo.Counties];
delete counties
go
insert Counties
select distinct ltrim(rtrim([county])), ltrim(rtrim([state])) from Property
go

alter table States drop constraint  [PK_dbo.States];
delete states
go
insert states
select distinct ltrim(rtrim([state])) from Property
go

--delete PropertyValueRanges
--go
--insert PropertyValueRanges
--select
--        concat('$', min(value / 1000), 'K - $', max(value / 1000), 'K') as [label],
--        min(value) as [min],
--        max(value) as [max]
--from
--        (
--                select distinct
--                        (row_number() over (order by propertyvalue)) / (select count(distinct [PropertyValue]) / 10 from property) as id,
--                        propertyvalue as value
--                from
--                        (select distinct [PropertyValue] from property) as p 
--        ) as z
--group by id
--order by [min]
--go

delete HomeValueRanges
go
insert HomeValueRanges
select
        concat('$', min(value / 1000), 'K - $', max(value / 1000), 'K') as [label],
        min(value) as [min],
        max(value) as [max]
from
        (
                select distinct
                        (row_number() over (order by homevalue)) / (select count(distinct homevalue) / 10 from persons) as id,
                        homevalue as value
                from
                        (select distinct homevalue from persons) as p 
        ) as z
group by id
order by [min]
go

delete LoanAmountRanges 
go
insert LoanAmountRanges
select
        concat('$', min(value / 1000), 'K - $', max(value / 1000), 'K') as [label],
        min(value) as [min],
        max(value) as [max]
from
        (
                select distinct
                        (row_number() over (order by loanamount)) / (select count(distinct loanamount) / 10 from mortgages) as id,
                        loanamount as value
                from
                        (select distinct loanamount from mortgages) as p 
        ) as z
group by id
order by [min]
go

Delete dbo.[LoanToValueRanges]
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'0% - 19.99%', 0, 19.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'20% - 39.99%', 20, 39.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'40% - 59.99%', 40, 59.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'60% - 79.99%', 60, 79.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'80% - 99.99%', 80, 99.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'100% - 119.99%', 100, 119.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'120% - 139.99%', 120, 139.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'140% - 159.99%', 140, 159.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'160% - 179.99%', 160, 179.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'180% - 199.99%', 180, 199.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'200% - 219.99%', 200, 219.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'220% - 239.99%', 220, 239.99)
INSERT [dbo].[LoanToValueRanges] ([Label], [StartValue], [EndValue]) VALUES (N'240% - 259.99%', 240, 259.99)
Go

delete LoanTypes
go
insert LoanTypes
select distinct LoanType from Mortgages where not LoanType is null
go

delete MortgateTypes 
go
insert MortgateTypes
select distinct MortgageType from Mortgages where not MortgageType is null
go

delete InterestRateTypes 
go
insert InterestRateTypes
select distinct InterestRateType from Mortgages where not InterestRateType is null
go

update Mortgages
set LoanAmountRange = (select label from [dbo].[LoanAmountRanges] where StartValue <= LoanAmount and LoanAmount <= EndValue)
go

update Mortgages
set LoanToValueRange = (select label from [dbo].[LoanToValueRanges] where StartValue <= LoanToValue and LoanToValue <= EndValue)
go

update Persons
set HomeValueRange = (select label from [dbo].[HomeValueRanges] where StartValue <= HomeValue and HomeValue <= EndValue)
go

--update Property
--set PropertyValueRange = (select label from [dbo].[PropertyValueRanges] where StartValue <= PropertyValue and PropertyValue <= EndValue)
--go


select distinct * into #Tmp1 from Persons
drop table Persons
select * into Persons from #Tmp1
drop table #Tmp1

select distinct * into #Tmp2 from Mortgages
drop table Mortgages
select * into Mortgages from #Tmp2
drop table #Tmp2

select distinct * into #Tmp3 from Property
drop table Property
select * into Property from #Tmp3
drop table #Tmp3