declare @feed sysname
set @feed = 'Reverse Database'

delete from [dbo].[Mortgages]
where MortgageId in 
(
	select MortgageId from [dbo].[BackBone] 
	where FeedId in(select FeedId from [mgt].[FeedDetails] where FeedFile = @feed)
)

delete from [dbo].[Persons]
where PersonId in 
(
	select PersonId from [dbo].[BackBone] 
	where FeedId in(select FeedId from [mgt].[FeedDetails] where FeedFile = @feed)
)

delete from [dbo].[Property]
where PropertyId in 
(
	select PropertyId from [dbo].[BackBone] 
	where FeedId in(select FeedId from [mgt].[FeedDetails] where FeedFile = @feed)
)

delete from [dbo].[BackBone] 
where FeedId in(select FeedId from [mgt].[FeedDetails] where FeedFile = @feed)