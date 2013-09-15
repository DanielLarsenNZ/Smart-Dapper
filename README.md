# Smart Data Access with Dapper
(Presentation and code examples).

Sometimes ORMs can make you feel like you are losing the battle with complexity. Dapper (and other Micro ORMs like it) can free up your 
stack and increase productivity by breathing new life into good old IDbConnection. In this session I will drill down into the powerful 
simplicity of Dapper and show how, when combined with Domain Driven Design, this natty little class file can even liberate your old legacy code.

@DanielLarsenNZ is a Development Director at Spendvision. He trains and mentors Developers in Agile Software Development, Domain Driven Design and .NET.

## Outline

* Welcome
* Today's presentation
* Software design is a constant battle with complexity
* About Dapper
 * 3 Helpers
 * How does it work?
 * Demo
* In the stack - Dapper at work
* My stack
 * The magic of ORMs
 * The pain of ORMs
 * Case study - The YTD sales report
  * Sales ERD
  * Demo
* Falling back in love with SQL again
* Getting Dapper into the stack
 * CQRS
 * Poor man's CQRS
 * Bounded context
 * Anti-corruption layer
 * Any ADO provider
 * Demo
* Summary
* Thank you
* Sponsors

## Install and run Code Samples

1. Download and install an AdventureWorks database from http://msftdbprodsamples.codeplex.com/releases/view/93587
2. Modify the web.config file connection strings, and optionally the test project app.config file as well.
3. Compile and F5 to run in the browser.

## References and links

+ http://code.google.com/p/dapper-dot-net/
+ https://github.com/tmsmith/Dapper-Extensions
+ http://miniprofiler.com/
+ http://blog.sqlauthority.com/2013/04/14/sql-server-tricks-for-row-offset-and-paging-in-various-versions-of-sql-server/
+ http://msdn.microsoft.com/en-us/data/gg699321.aspx - How to use EF to run query proc that returns Entity
+ http://msdn.microsoft.com/en-us/data/dd363565.aspx - ADO.NET Data Providers
+ http://martinfowler.com/bliki/CQRS.html
+ http://samsaffron.com/archive/2011/03/30/How+I+learned+to+stop+worrying+and+write+my+own+ORM
+ http://merc.tv/img/fig/AdventureWorks2008.gif - ERD
+ https://gist.github.com/SamSaffron/893878 - Sam's original gist

## History

* 15-Sep-2013 - Presented at Microsoft Communities Code Camp 2013 (Auckland, NZ) - http://www.mscommunities.co.nz/CodeCamp.aspx


## License
http://opensource.org/licenses/MIT (see LICENSE.txt).