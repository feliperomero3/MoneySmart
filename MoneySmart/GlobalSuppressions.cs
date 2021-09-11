// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "@davidfowl: 'It's how console applications always worked.'", Scope = "type", Target = "~T:MoneySmart.Program")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "This is the standard method signature included in the new project template.", Scope = "member", Target = "~M:MoneySmart.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]
[assembly: SuppressMessage("Blocker Code Smell", "S3875:\"operator==\" should not be overloaded on reference types", Justification = "This type is for domain driven design", Scope = "member", Target = "~M:MoneySmart.Entities.Entity.op_Equality(MoneySmart.Entities.Entity,MoneySmart.Entities.Entity)~System.Boolean")]
[assembly: SuppressMessage("Code Smell", "S2068: \"password\" detected here, make sure this is not a hard-coded credential.", Justification = "These are the default administrator credentials.", Scope = "type", Target = "~T:MoneySmart.Data.ApplicationDbSeedData")]
