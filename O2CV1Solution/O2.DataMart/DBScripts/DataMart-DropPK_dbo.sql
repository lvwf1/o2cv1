use [DataMart_FHA01] 
alter table BackBone drop constraint  [FK_dbo.BackBone_dbo.Persons_PersonId];
alter table BackBone drop constraint  [FK_dbo.BackBone_dbo.Mortgages_MortgageId];
alter table BackBone drop constraint  [FK_dbo.BackBone_dbo.Property_PropertyId];
alter table Persons drop constraint  [PK_dbo.Persons];
alter table Mortgages drop constraint  [PK_dbo.Mortgages];
alter table Property drop constraint  [PK_dbo.Property];