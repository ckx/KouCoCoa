// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "Simple 'using' statements give less guarantee of when Dispose() is called as the using block's scope can become ambiguous. They also don't adhere to 1TBS.", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "new(...) is good, but if I don't want to use it sometimes, then I don't want a compiler message about it.", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "This one always gets me. Like, really, VS? A compiler message nagging about the default Main() parameters...?", Scope = "member", Target = "~M:KouCoCoa.Program.Main(System.String[])")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Clear initialization is good whether it's necessary or not.", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE0057:Use range operator", Justification = "Range operator is completely fine, but can be less readable. Dunno why equal alternatives are always presented by these nagging messages as 'better' in VS.", Scope = "module")]