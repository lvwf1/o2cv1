/* this script replicates out the data Input_Example until it has at least 2 million rows */

while (select count(*) from Input_Example) < 2000000
insert Input_Example select * from Input_Example