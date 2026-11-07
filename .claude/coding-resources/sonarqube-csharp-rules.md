# SonarQube C# rules (external copy)

A downloaded list of SonarSource's C# rules (S100 and up): the messages the `SonarAnalyzer.CSharp` package gives in
Visual Studio. "On by default (Sonar way)" says whether the package switches the rule on without any setting. "On in
this project" also takes the `dotnet_diagnostic.Sxxx.severity` lines in the `.editorconfig` into account. Everything
below the end-of-header line is generated from SonarSource's own rule files and the `.editorconfig`.

- Source: https://rules.sonarsource.com/csharp
- Raw files: https://github.com/SonarSource/sonar-dotnet/tree/master/analyzers/rspec/cs

To update the contents below this header, run this from the repository root in PowerShell:

```powershell
$file = 'docs/sonarqube-csharp-rules.md'
$repo = 'https://raw.githubusercontent.com/SonarSource/sonar-dotnet/master/analyzers/rspec/cs'
$header = ((Get-Content $file -Raw) -split '(?m)^<!-- end of header -->\r?$', 2)[0]
$tree = Invoke-RestMethod 'https://api.github.com/repos/SonarSource/sonar-dotnet/git/trees/master:analyzers/rspec/cs'
$sonarWay = (Invoke-RestMethod "$repo/Sonar_way_profile.json").ruleKeys
$overrides = @{}
Get-Content '.editorconfig' | ForEach-Object {
    if ($_ -match '^dotnet_diagnostic\.(S\d+)\.severity\s*=\s*(\S+)') { $overrides[$Matches[1]] = $Matches[2] }
}
$rows = $tree.tree.path | Where-Object { $_ -match '^S\d+\.json$' } | Sort-Object { [int]($_ -replace '\D') } | ForEach-Object {
    $rule = Invoke-RestMethod "$repo/$_"
    $key = $_ -replace '\.json$'
    $default = if ($sonarWay -contains $key) { 'Yes' } else { 'No' }
    $here = if (-not $overrides.ContainsKey($key)) { $default } elseif ($overrides[$key] -eq 'none') { 'No' } else { 'Yes' }
    "| [$key](https://rules.sonarsource.com/csharp/RSPEC-$($key.Substring(1))) | $($rule.title -replace '\|', '\|') | $($rule.type) | $($rule.defaultSeverity) | $($rule.status) | $default | $here |"
}
$body = @('| Rule | Title | Type | Severity | Status | On by default (Sonar way) | On in this project |', '| - | - | - | - | - | - | - |') + $rows
Set-Content $file ($header + "<!-- end of header -->`n`n" + ($body -join "`n") + "`n") -NoNewline -Encoding utf8
```

<!-- end of header -->

| Rule | Title | Type | Severity | Status | On by default (Sonar way) | On in this project |
| - | - | - | - | - | - | - |
| [S100](https://rules.sonarsource.com/csharp/RSPEC-100) | Methods and properties should be named in PascalCase | CODE_SMELL | Minor | ready | No | No |
| [S101](https://rules.sonarsource.com/csharp/RSPEC-101) | Types should be named in PascalCase | CODE_SMELL | Minor | ready | Yes | Yes |
| [S103](https://rules.sonarsource.com/csharp/RSPEC-103) | Lines should not be too long | CODE_SMELL | Major | ready | No | No |
| [S104](https://rules.sonarsource.com/csharp/RSPEC-104) | Files should not have too many lines of code | CODE_SMELL | Major | ready | No | No |
| [S105](https://rules.sonarsource.com/csharp/RSPEC-105) | Tabulation characters should not be used | CODE_SMELL | Minor | ready | No | No |
| [S106](https://rules.sonarsource.com/csharp/RSPEC-106) | Standard outputs should not be used directly to log anything | CODE_SMELL | Major | ready | No | No |
| [S107](https://rules.sonarsource.com/csharp/RSPEC-107) | Methods should not have too many parameters | CODE_SMELL | Major | ready | Yes | Yes |
| [S108](https://rules.sonarsource.com/csharp/RSPEC-108) | Nested blocks of code should not be left empty | CODE_SMELL | Major | ready | Yes | No |
| [S109](https://rules.sonarsource.com/csharp/RSPEC-109) | Magic numbers should not be used | CODE_SMELL | Major | ready | No | Yes |
| [S110](https://rules.sonarsource.com/csharp/RSPEC-110) | Inheritance tree of classes should not be too deep | CODE_SMELL | Major | ready | Yes | Yes |
| [S112](https://rules.sonarsource.com/csharp/RSPEC-112) | General or reserved exceptions should never be thrown | CODE_SMELL | Major | ready | Yes | Yes |
| [S113](https://rules.sonarsource.com/csharp/RSPEC-113) | Files should end with a newline | CODE_SMELL | Minor | ready | No | No |
| [S121](https://rules.sonarsource.com/csharp/RSPEC-121) | Control structures should use curly braces | CODE_SMELL | Critical | ready | No | Yes |
| [S122](https://rules.sonarsource.com/csharp/RSPEC-122) | Statements should be on separate lines | CODE_SMELL | Major | ready | No | Yes |
| [S125](https://rules.sonarsource.com/csharp/RSPEC-125) | Sections of code should not be commented out | CODE_SMELL | Major | ready | Yes | Yes |
| [S126](https://rules.sonarsource.com/csharp/RSPEC-126) | "if ... else if" constructs should end with "else" clauses | CODE_SMELL | Critical | ready | No | Yes |
| [S127](https://rules.sonarsource.com/csharp/RSPEC-127) | "for" loop stop conditions should be invariant | CODE_SMELL | Major | ready | Yes | Yes |
| [S131](https://rules.sonarsource.com/csharp/RSPEC-131) | "switch/Select" statements should contain a "default/Case Else" clauses | CODE_SMELL | Critical | ready | No | Yes |
| [S134](https://rules.sonarsource.com/csharp/RSPEC-134) | Control flow statements "if", "switch", "for", "foreach", "while", "do"  and "try" should not be nested too deeply | CODE_SMELL | Critical | ready | No | Yes |
| [S138](https://rules.sonarsource.com/csharp/RSPEC-138) | Functions should not have too many lines of code | CODE_SMELL | Major | ready | No | Yes |
| [S818](https://rules.sonarsource.com/csharp/RSPEC-818) | Literal suffixes should be upper case | CODE_SMELL | Minor | ready | Yes | Yes |
| [S881](https://rules.sonarsource.com/csharp/RSPEC-881) | Increment (++) and decrement (--) operators should not be used in a method call or mixed with other operators in an expression | CODE_SMELL | Major | ready | No | No |
| [S907](https://rules.sonarsource.com/csharp/RSPEC-907) | "goto" statement should not be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S927](https://rules.sonarsource.com/csharp/RSPEC-927) | Parameter names should match base declaration and other partial definitions | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1006](https://rules.sonarsource.com/csharp/RSPEC-1006) | Method overrides should not change parameter defaults | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1048](https://rules.sonarsource.com/csharp/RSPEC-1048) | Finalizers should not throw exceptions | BUG | Blocker | ready | Yes | Yes |
| [S1066](https://rules.sonarsource.com/csharp/RSPEC-1066) | Mergeable "if" statements should be combined | CODE_SMELL | Major | ready | Yes | Yes |
| [S1067](https://rules.sonarsource.com/csharp/RSPEC-1067) | Expressions should not be too complex | CODE_SMELL | Critical | ready | No | Yes |
| [S1075](https://rules.sonarsource.com/csharp/RSPEC-1075) | URIs should not be hardcoded | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1104](https://rules.sonarsource.com/csharp/RSPEC-1104) | Fields should not have public accessibility | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1109](https://rules.sonarsource.com/csharp/RSPEC-1109) | A close curly brace should be located at the beginning of a line | CODE_SMELL | Minor | ready | No | No |
| [S1110](https://rules.sonarsource.com/csharp/RSPEC-1110) | Redundant pairs of parentheses should be removed | CODE_SMELL | Major | ready | Yes | Yes |
| [S1116](https://rules.sonarsource.com/csharp/RSPEC-1116) | Empty statements should be removed | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1117](https://rules.sonarsource.com/csharp/RSPEC-1117) | Local variables should not shadow class fields or properties | CODE_SMELL | Major | ready | Yes | Yes |
| [S1118](https://rules.sonarsource.com/csharp/RSPEC-1118) | Utility classes should not have public constructors | CODE_SMELL | Major | ready | Yes | Yes |
| [S1121](https://rules.sonarsource.com/csharp/RSPEC-1121) | Assignments should not be made from within sub-expressions | CODE_SMELL | Major | ready | Yes | Yes |
| [S1123](https://rules.sonarsource.com/csharp/RSPEC-1123) | "Obsolete" attributes should include explanations | CODE_SMELL | Major | ready | Yes | Yes |
| [S1125](https://rules.sonarsource.com/csharp/RSPEC-1125) | Boolean literals should not be redundant | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1128](https://rules.sonarsource.com/csharp/RSPEC-1128) | Unnecessary "using" should be removed | CODE_SMELL | Minor | ready | No | No |
| [S1133](https://rules.sonarsource.com/csharp/RSPEC-1133) | Deprecated code should be removed | CODE_SMELL | Info | ready | Yes | Yes |
| [S1134](https://rules.sonarsource.com/csharp/RSPEC-1134) | Track uses of "FIXME" tags | CODE_SMELL | Major | ready | Yes | Yes |
| [S1135](https://rules.sonarsource.com/csharp/RSPEC-1135) | Track uses of "TODO" tags | CODE_SMELL | Info | ready | Yes | Yes |
| [S1144](https://rules.sonarsource.com/csharp/RSPEC-1144) | Unused private types or members should be removed | CODE_SMELL | Major | ready | Yes | Yes |
| [S1147](https://rules.sonarsource.com/csharp/RSPEC-1147) | Exit methods should not be called | CODE_SMELL | Blocker | ready | No | No |
| [S1151](https://rules.sonarsource.com/csharp/RSPEC-1151) | "switch case" clauses should not have too many lines of code | CODE_SMELL | Major | ready | No | No |
| [S1155](https://rules.sonarsource.com/csharp/RSPEC-1155) | "Any()" should be used to test for emptiness | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1163](https://rules.sonarsource.com/csharp/RSPEC-1163) | Exceptions should not be thrown in finally blocks | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1168](https://rules.sonarsource.com/csharp/RSPEC-1168) | Empty arrays and collections should be returned instead of null | CODE_SMELL | Major | ready | Yes | Yes |
| [S1172](https://rules.sonarsource.com/csharp/RSPEC-1172) | Unused method parameters should be removed | CODE_SMELL | Major | ready | Yes | Yes |
| [S1185](https://rules.sonarsource.com/csharp/RSPEC-1185) | Overriding members should do more than simply call the same member in the base class | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1186](https://rules.sonarsource.com/csharp/RSPEC-1186) | Methods should not be empty | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1192](https://rules.sonarsource.com/csharp/RSPEC-1192) | String literals should not be duplicated | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1199](https://rules.sonarsource.com/csharp/RSPEC-1199) | Nested code blocks should not be used | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1200](https://rules.sonarsource.com/csharp/RSPEC-1200) | Classes should not be coupled to too many other classes | CODE_SMELL | Major | ready | No | No |
| [S1206](https://rules.sonarsource.com/csharp/RSPEC-1206) | "Equals(Object)" and "GetHashCode()" should be overridden in pairs | BUG | Minor | ready | Yes | Yes |
| [S1210](https://rules.sonarsource.com/csharp/RSPEC-1210) | "Equals" and the comparison operators should be overridden when implementing "IComparable" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1215](https://rules.sonarsource.com/csharp/RSPEC-1215) | "GC.Collect" should not be called | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1226](https://rules.sonarsource.com/csharp/RSPEC-1226) | Method parameters, caught exceptions and foreach variables' initial values should not be ignored | BUG | Minor | ready | No | No |
| [S1227](https://rules.sonarsource.com/csharp/RSPEC-1227) | break statements should not be used except for switch cases | CODE_SMELL | Minor | deprecated | No | No |
| [S1244](https://rules.sonarsource.com/csharp/RSPEC-1244) | Floating point numbers should not be tested for equality | BUG | Major | ready | Yes | Yes |
| [S1264](https://rules.sonarsource.com/csharp/RSPEC-1264) | A "while" loop should be used instead of a "for" loop | CODE_SMELL | Minor | ready | No | No |
| [S1301](https://rules.sonarsource.com/csharp/RSPEC-1301) | "switch" statements should have at least 3 "case" clauses | CODE_SMELL | Minor | ready | No | No |
| [S1309](https://rules.sonarsource.com/csharp/RSPEC-1309) | Track uses of in-source issue suppressions | CODE_SMELL | Info | ready | No | No |
| [S1312](https://rules.sonarsource.com/csharp/RSPEC-1312) | Logger fields should be "private static readonly" | CODE_SMELL | Minor | ready | No | No |
| [S1313](https://rules.sonarsource.com/csharp/RSPEC-1313) | IP addresses should not be hardcoded | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1449](https://rules.sonarsource.com/csharp/RSPEC-1449) | Culture should be specified for "string" operations | CODE_SMELL | Minor | ready | No | No |
| [S1450](https://rules.sonarsource.com/csharp/RSPEC-1450) | Private fields only used as local variables in methods should become local variables | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1451](https://rules.sonarsource.com/csharp/RSPEC-1451) | Track lack of copyright and license headers | CODE_SMELL | Blocker | ready | No | No |
| [S1479](https://rules.sonarsource.com/csharp/RSPEC-1479) | "switch" statements with many "case" clauses should have only one statement | CODE_SMELL | Major | ready | Yes | Yes |
| [S1481](https://rules.sonarsource.com/csharp/RSPEC-1481) | Unused local variables should be removed | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1541](https://rules.sonarsource.com/csharp/RSPEC-1541) | Methods and properties should not be too complex | CODE_SMELL | Critical | ready | No | No |
| [S1607](https://rules.sonarsource.com/csharp/RSPEC-1607) | Tests should not be ignored | CODE_SMELL | Major | ready | Yes | Yes |
| [S1643](https://rules.sonarsource.com/csharp/RSPEC-1643) | Strings should not be concatenated using '+' in a loop | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1656](https://rules.sonarsource.com/csharp/RSPEC-1656) | Variables should not be self-assigned | BUG | Major | ready | Yes | Yes |
| [S1659](https://rules.sonarsource.com/csharp/RSPEC-1659) | Multiple variables should not be declared on the same line | CODE_SMELL | Minor | ready | No | No |
| [S1694](https://rules.sonarsource.com/csharp/RSPEC-1694) | An abstract class should have both abstract and concrete methods | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1696](https://rules.sonarsource.com/csharp/RSPEC-1696) | NullReferenceException should not be caught | CODE_SMELL | Major | ready | Yes | Yes |
| [S1698](https://rules.sonarsource.com/csharp/RSPEC-1698) | "==" should not be used when "Equals" is overridden | CODE_SMELL | Minor | ready | No | No |
| [S1699](https://rules.sonarsource.com/csharp/RSPEC-1699) | Constructors should only call non-overridable methods | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1751](https://rules.sonarsource.com/csharp/RSPEC-1751) | Loops with at most one iteration should be refactored | BUG | Major | ready | Yes | Yes |
| [S1764](https://rules.sonarsource.com/csharp/RSPEC-1764) | Identical expressions should not be used on both sides of operators | BUG | Major | ready | Yes | Yes |
| [S1821](https://rules.sonarsource.com/csharp/RSPEC-1821) | "switch" statements should not be nested | CODE_SMELL | Critical | ready | No | No |
| [S1848](https://rules.sonarsource.com/csharp/RSPEC-1848) | Objects should not be created to be dropped immediately without being used | BUG | Major | ready | Yes | Yes |
| [S1854](https://rules.sonarsource.com/csharp/RSPEC-1854) | Unused assignments should be removed | CODE_SMELL | Major | ready | Yes | Yes |
| [S1858](https://rules.sonarsource.com/csharp/RSPEC-1858) | "ToString()" calls should not be redundant | CODE_SMELL | Minor | ready | No | No |
| [S1862](https://rules.sonarsource.com/csharp/RSPEC-1862) | Related "if/else if" statements should not have the same condition | BUG | Major | ready | Yes | Yes |
| [S1871](https://rules.sonarsource.com/csharp/RSPEC-1871) | Two branches in a conditional structure should not have exactly the same implementation | CODE_SMELL | Major | ready | Yes | Yes |
| [S1905](https://rules.sonarsource.com/csharp/RSPEC-1905) | Redundant casts should not be used | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1939](https://rules.sonarsource.com/csharp/RSPEC-1939) | Inheritance list should not be redundant | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1940](https://rules.sonarsource.com/csharp/RSPEC-1940) | Boolean checks should not be inverted | CODE_SMELL | Minor | ready | Yes | Yes |
| [S1944](https://rules.sonarsource.com/csharp/RSPEC-1944) | Invalid casts should be avoided | CODE_SMELL | Critical | ready | Yes | Yes |
| [S1994](https://rules.sonarsource.com/csharp/RSPEC-1994) | "for" loop increment clauses should modify the loops' counters | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2053](https://rules.sonarsource.com/csharp/RSPEC-2053) | Password hashing functions should use an unpredictable salt | VULNERABILITY | Critical | ready | Yes | Yes |
| [S2068](https://rules.sonarsource.com/csharp/RSPEC-2068) | Credentials should not be hard-coded | VULNERABILITY | Major | ready | Yes | Yes |
| [S2077](https://rules.sonarsource.com/csharp/RSPEC-2077) | SQL queries should not be dynamically formatted | VULNERABILITY | Major | ready | Yes | Yes |
| [S2092](https://rules.sonarsource.com/csharp/RSPEC-2092) | Cookies should have the "secure" flag | VULNERABILITY | Minor | ready | Yes | Yes |
| [S2094](https://rules.sonarsource.com/csharp/RSPEC-2094) | Classes should not be empty | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2114](https://rules.sonarsource.com/csharp/RSPEC-2114) | Collections should not be passed as arguments to their own methods | BUG | Major | ready | Yes | Yes |
| [S2115](https://rules.sonarsource.com/csharp/RSPEC-2115) | A secure password should be used when connecting to a database | VULNERABILITY | Blocker | ready | Yes | Yes |
| [S2123](https://rules.sonarsource.com/csharp/RSPEC-2123) | Values should not be uselessly incremented | BUG | Major | ready | Yes | Yes |
| [S2139](https://rules.sonarsource.com/csharp/RSPEC-2139) | Exceptions should be either logged or rethrown but not both | CODE_SMELL | Major | ready | Yes | Yes |
| [S2148](https://rules.sonarsource.com/csharp/RSPEC-2148) | Underscores should be used to make large numbers readable | CODE_SMELL | Minor | ready | No | No |
| [S2156](https://rules.sonarsource.com/csharp/RSPEC-2156) | "sealed" classes should not have "protected" members | CODE_SMELL | Minor | ready | No | No |
| [S2166](https://rules.sonarsource.com/csharp/RSPEC-2166) | Classes named like "Exception" should extend "Exception" or a subclass | CODE_SMELL | Major | ready | Yes | Yes |
| [S2178](https://rules.sonarsource.com/csharp/RSPEC-2178) | Short-circuit logic should be used in boolean contexts | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2183](https://rules.sonarsource.com/csharp/RSPEC-2183) | Integral numbers should not be shifted by zero or more than their number of bits-1 | BUG | Minor | ready | Yes | Yes |
| [S2184](https://rules.sonarsource.com/csharp/RSPEC-2184) | Results of integer division should not be assigned to floating point variables | BUG | Minor | ready | Yes | Yes |
| [S2187](https://rules.sonarsource.com/csharp/RSPEC-2187) | Test classes should contain at least one test case | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2190](https://rules.sonarsource.com/csharp/RSPEC-2190) | Loops and recursions should not be infinite | BUG | Blocker | ready | Yes | Yes |
| [S2197](https://rules.sonarsource.com/csharp/RSPEC-2197) | Modulus results should not be checked for direct equality | CODE_SMELL | Critical | ready | No | No |
| [S2198](https://rules.sonarsource.com/csharp/RSPEC-2198) | Unnecessary mathematical comparisons should not be made | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2201](https://rules.sonarsource.com/csharp/RSPEC-2201) | Methods without side effects should not have their return values ignored | BUG | Major | ready | Yes | Yes |
| [S2219](https://rules.sonarsource.com/csharp/RSPEC-2219) | Runtime type checking should be simplified | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2221](https://rules.sonarsource.com/csharp/RSPEC-2221) | "Exception" should not be caught | CODE_SMELL | Minor | ready | No | Yes |
| [S2222](https://rules.sonarsource.com/csharp/RSPEC-2222) | Locks should be released on all paths | BUG | Critical | ready | Yes | Yes |
| [S2223](https://rules.sonarsource.com/csharp/RSPEC-2223) | Non-constant static fields should not be visible | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2225](https://rules.sonarsource.com/csharp/RSPEC-2225) | "ToString()" method should not return null | BUG | Major | ready | Yes | Yes |
| [S2234](https://rules.sonarsource.com/csharp/RSPEC-2234) | Arguments should be passed in the same order as the method parameters | CODE_SMELL | Major | ready | Yes | Yes |
| [S2245](https://rules.sonarsource.com/csharp/RSPEC-2245) | Pseudorandom number generators (PRNGs) should not be used in security contexts | VULNERABILITY | Major | ready | Yes | Yes |
| [S2251](https://rules.sonarsource.com/csharp/RSPEC-2251) | A "for" loop update clause should move the counter in the right direction | BUG | Major | ready | Yes | Yes |
| [S2252](https://rules.sonarsource.com/csharp/RSPEC-2252) | For-loop conditions should be true at least once | BUG | Major | ready | Yes | Yes |
| [S2257](https://rules.sonarsource.com/csharp/RSPEC-2257) | Custom cryptographic algorithms should not be used | VULNERABILITY | Critical | ready | Yes | Yes |
| [S2259](https://rules.sonarsource.com/csharp/RSPEC-2259) | Null pointers should not be dereferenced | BUG | Major | ready | Yes | Yes |
| [S2275](https://rules.sonarsource.com/csharp/RSPEC-2275) | Composite format strings should not lead to unexpected behavior at runtime | BUG | Blocker | ready | Yes | Yes |
| [S2290](https://rules.sonarsource.com/csharp/RSPEC-2290) | Field-like events should not be virtual | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2291](https://rules.sonarsource.com/csharp/RSPEC-2291) | Overflow checking should not be disabled for "Enumerable.Sum" | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2292](https://rules.sonarsource.com/csharp/RSPEC-2292) | Trivial properties should be auto-implemented | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2302](https://rules.sonarsource.com/csharp/RSPEC-2302) | "nameof" should be used | CODE_SMELL | Critical | ready | No | No |
| [S2306](https://rules.sonarsource.com/csharp/RSPEC-2306) | "async" and "await" should not be used as identifiers | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2325](https://rules.sonarsource.com/csharp/RSPEC-2325) | Methods and properties that don't access instance data should be static | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2326](https://rules.sonarsource.com/csharp/RSPEC-2326) | Unused type parameters should be removed | CODE_SMELL | Major | ready | Yes | Yes |
| [S2327](https://rules.sonarsource.com/csharp/RSPEC-2327) | "try" statements with identical "catch" and/or "finally" blocks should be merged | CODE_SMELL | Major | ready | No | No |
| [S2328](https://rules.sonarsource.com/csharp/RSPEC-2328) | "GetHashCode" should not reference mutable fields | BUG | Minor | ready | Yes | Yes |
| [S2330](https://rules.sonarsource.com/csharp/RSPEC-2330) | Array covariance should not be used | CODE_SMELL | Critical | ready | No | No |
| [S2333](https://rules.sonarsource.com/csharp/RSPEC-2333) | Redundant modifiers should not be used | CODE_SMELL | Minor | ready | No | No |
| [S2339](https://rules.sonarsource.com/csharp/RSPEC-2339) | Public constant members should not be used | CODE_SMELL | Critical | ready | No | No |
| [S2342](https://rules.sonarsource.com/csharp/RSPEC-2342) | Enumeration types should comply with a naming convention | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2344](https://rules.sonarsource.com/csharp/RSPEC-2344) | Enumeration type names should not have "Flags" or "Enum" suffixes | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2345](https://rules.sonarsource.com/csharp/RSPEC-2345) | Flags enumerations should explicitly initialize all their members | BUG | Minor | ready | Yes | Yes |
| [S2346](https://rules.sonarsource.com/csharp/RSPEC-2346) | Flags enumerations zero-value members should be named "None" | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2357](https://rules.sonarsource.com/csharp/RSPEC-2357) | Fields should be private | CODE_SMELL | Major | ready | No | No |
| [S2360](https://rules.sonarsource.com/csharp/RSPEC-2360) | Optional parameters should not be used | CODE_SMELL | Critical | ready | No | Yes |
| [S2365](https://rules.sonarsource.com/csharp/RSPEC-2365) | Properties should not make collection or array copies | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2368](https://rules.sonarsource.com/csharp/RSPEC-2368) | Public methods should not have multidimensional array parameters | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2372](https://rules.sonarsource.com/csharp/RSPEC-2372) | Exceptions should not be thrown from property getters | CODE_SMELL | Major | ready | Yes | Yes |
| [S2376](https://rules.sonarsource.com/csharp/RSPEC-2376) | Write-only properties should not be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S2386](https://rules.sonarsource.com/csharp/RSPEC-2386) | Mutable fields should not be "public static" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2387](https://rules.sonarsource.com/csharp/RSPEC-2387) | Child class fields should not shadow parent class fields | CODE_SMELL | Blocker | deprecated | No | No |
| [S2436](https://rules.sonarsource.com/csharp/RSPEC-2436) | Types and methods should not have too many generic parameters | CODE_SMELL | Major | ready | Yes | Yes |
| [S2437](https://rules.sonarsource.com/csharp/RSPEC-2437) | Unnecessary bit operations should not be performed | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2445](https://rules.sonarsource.com/csharp/RSPEC-2445) | Blocks should be synchronized on read-only fields | BUG | Major | ready | Yes | Yes |
| [S2479](https://rules.sonarsource.com/csharp/RSPEC-2479) | Whitespace and control characters in string literals should be explicit | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2486](https://rules.sonarsource.com/csharp/RSPEC-2486) | Generic exceptions should not be ignored | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2551](https://rules.sonarsource.com/csharp/RSPEC-2551) | Shared resources should not be used for locking | BUG | Critical | ready | Yes | Yes |
| [S2583](https://rules.sonarsource.com/csharp/RSPEC-2583) | Conditionally executed code should be reachable | BUG | Major | ready | Yes | Yes |
| [S2589](https://rules.sonarsource.com/csharp/RSPEC-2589) | Boolean expressions should not be gratuitous | CODE_SMELL | Major | ready | Yes | Yes |
| [S2612](https://rules.sonarsource.com/csharp/RSPEC-2612) | File permissions should not be set to world-accessible values | VULNERABILITY | Major | ready | Yes | Yes |
| [S2629](https://rules.sonarsource.com/csharp/RSPEC-2629) | Logging templates should be constant | CODE_SMELL | Major | ready | Yes | Yes |
| [S2674](https://rules.sonarsource.com/csharp/RSPEC-2674) | The length returned from a stream read should be checked | BUG | Minor | ready | Yes | Yes |
| [S2681](https://rules.sonarsource.com/csharp/RSPEC-2681) | Multiline blocks should be enclosed in curly braces | CODE_SMELL | Major | ready | Yes | Yes |
| [S2688](https://rules.sonarsource.com/csharp/RSPEC-2688) | "NaN" should not be used in comparisons | BUG | Major | ready | Yes | Yes |
| [S2692](https://rules.sonarsource.com/csharp/RSPEC-2692) | "IndexOf" checks should not be for positive numbers | CODE_SMELL | Critical | ready | No | No |
| [S2696](https://rules.sonarsource.com/csharp/RSPEC-2696) | Instance members should not write to "static" fields | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2699](https://rules.sonarsource.com/csharp/RSPEC-2699) | Tests should include assertions | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2701](https://rules.sonarsource.com/csharp/RSPEC-2701) | Literal boolean values should not be used in assertions | CODE_SMELL | Critical | ready | Yes | Yes |
| [S2737](https://rules.sonarsource.com/csharp/RSPEC-2737) | "catch" clauses should do more than rethrow | CODE_SMELL | Minor | ready | Yes | Yes |
| [S2743](https://rules.sonarsource.com/csharp/RSPEC-2743) | Static fields should not be used in generic types | CODE_SMELL | Major | ready | Yes | Yes |
| [S2755](https://rules.sonarsource.com/csharp/RSPEC-2755) | XML parsers should not be vulnerable to XXE attacks | VULNERABILITY | Blocker | ready | Yes | Yes |
| [S2757](https://rules.sonarsource.com/csharp/RSPEC-2757) | Non-existent operators like "=+" should not be used | BUG | Major | ready | Yes | Yes |
| [S2760](https://rules.sonarsource.com/csharp/RSPEC-2760) | Sequential tests should not check the same condition | CODE_SMELL | Minor | ready | No | No |
| [S2761](https://rules.sonarsource.com/csharp/RSPEC-2761) | Doubled prefix operators "!!" and "~~" should not be used | BUG | Major | ready | Yes | Yes |
| [S2857](https://rules.sonarsource.com/csharp/RSPEC-2857) | SQL keywords should be delimited by whitespace | BUG | Blocker | ready | Yes | Yes |
| [S2925](https://rules.sonarsource.com/csharp/RSPEC-2925) | "Thread.Sleep" should not be used in tests | CODE_SMELL | Major | ready | Yes | Yes |
| [S2930](https://rules.sonarsource.com/csharp/RSPEC-2930) | "IDisposables" should be disposed | BUG | Blocker | ready | Yes | Yes |
| [S2931](https://rules.sonarsource.com/csharp/RSPEC-2931) | Classes with "IDisposable" members should implement "IDisposable" | BUG | Blocker | ready | No | Yes |
| [S2933](https://rules.sonarsource.com/csharp/RSPEC-2933) | Fields that are only assigned in the constructor should be "readonly" | CODE_SMELL | Major | ready | Yes | Yes |
| [S2934](https://rules.sonarsource.com/csharp/RSPEC-2934) | Property assignments should not be made for "readonly" fields not constrained to reference types | BUG | Minor | ready | Yes | Yes |
| [S2952](https://rules.sonarsource.com/csharp/RSPEC-2952) | Classes should "Dispose" of members from the classes' own "Dispose" methods | BUG | Critical | ready | No | Yes |
| [S2953](https://rules.sonarsource.com/csharp/RSPEC-2953) | Methods named "Dispose" should implement "IDisposable.Dispose" | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2955](https://rules.sonarsource.com/csharp/RSPEC-2955) | Generic parameters not constrained to reference types should not be compared to "null" | BUG | Minor | ready | Yes | Yes |
| [S2970](https://rules.sonarsource.com/csharp/RSPEC-2970) | Assertions should be complete | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S2971](https://rules.sonarsource.com/csharp/RSPEC-2971) | LINQ expressions should be simplified | CODE_SMELL | Major | ready | Yes | Yes |
| [S2995](https://rules.sonarsource.com/csharp/RSPEC-2995) | "Object.ReferenceEquals" should not be used for value types | BUG | Major | ready | Yes | Yes |
| [S2996](https://rules.sonarsource.com/csharp/RSPEC-2996) | "ThreadStatic" fields should not be initialized | BUG | Major | ready | Yes | Yes |
| [S2997](https://rules.sonarsource.com/csharp/RSPEC-2997) | "IDisposables" created in a "using" statement should not be returned | BUG | Major | ready | Yes | Yes |
| [S3005](https://rules.sonarsource.com/csharp/RSPEC-3005) | "ThreadStatic" should not be used on non-static fields | BUG | Major | ready | Yes | Yes |
| [S3010](https://rules.sonarsource.com/csharp/RSPEC-3010) | Static fields should not be updated in constructors | CODE_SMELL | Major | ready | Yes | Yes |
| [S3011](https://rules.sonarsource.com/csharp/RSPEC-3011) | Reflection should not be used to increase accessibility of classes, methods, or fields | CODE_SMELL | Major | ready | Yes | Yes |
| [S3052](https://rules.sonarsource.com/csharp/RSPEC-3052) | Members should not be initialized to default values | CODE_SMELL | Minor | ready | No | Yes |
| [S3059](https://rules.sonarsource.com/csharp/RSPEC-3059) | Types should not have members with visibility set higher than the type's visibility | CODE_SMELL | Major | ready | No | No |
| [S3060](https://rules.sonarsource.com/csharp/RSPEC-3060) | "is" should not be used with "this" | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3063](https://rules.sonarsource.com/csharp/RSPEC-3063) | "StringBuilder" data should be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S3168](https://rules.sonarsource.com/csharp/RSPEC-3168) | "async" methods should not return "void" | BUG | Major | ready | Yes | Yes |
| [S3169](https://rules.sonarsource.com/csharp/RSPEC-3169) | Multiple "OrderBy" calls should not be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S3172](https://rules.sonarsource.com/csharp/RSPEC-3172) | Delegates should not be subtracted | BUG | Major | ready | Yes | Yes |
| [S3215](https://rules.sonarsource.com/csharp/RSPEC-3215) | "interface" instances should not be cast to concrete types | CODE_SMELL | Critical | ready | No | No |
| [S3216](https://rules.sonarsource.com/csharp/RSPEC-3216) | "ConfigureAwait(false)" should be used | CODE_SMELL | Critical | ready | No | No |
| [S3217](https://rules.sonarsource.com/csharp/RSPEC-3217) | "Explicit" conversions of "foreach" loops should not be used | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3218](https://rules.sonarsource.com/csharp/RSPEC-3218) | Inner class members should not shadow outer class "static" or type members | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3220](https://rules.sonarsource.com/csharp/RSPEC-3220) | Method calls should not resolve ambiguously to overloads with "params" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3234](https://rules.sonarsource.com/csharp/RSPEC-3234) | "GC.SuppressFinalize" should not be invoked for types without destructors | CODE_SMELL | Minor | ready | No | No |
| [S3235](https://rules.sonarsource.com/csharp/RSPEC-3235) | Redundant parentheses should not be used | CODE_SMELL | Minor | ready | No | No |
| [S3236](https://rules.sonarsource.com/csharp/RSPEC-3236) | Caller information arguments should not be provided explicitly | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3237](https://rules.sonarsource.com/csharp/RSPEC-3237) | "value" contextual keyword should be used | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3240](https://rules.sonarsource.com/csharp/RSPEC-3240) | The simplest possible condition syntax should be used | CODE_SMELL | Minor | ready | No | No |
| [S3241](https://rules.sonarsource.com/csharp/RSPEC-3241) | Methods should not return values that are never used | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3242](https://rules.sonarsource.com/csharp/RSPEC-3242) | Method parameters should be declared with base types | CODE_SMELL | Minor | ready | No | No |
| [S3244](https://rules.sonarsource.com/csharp/RSPEC-3244) | Anonymous delegates should not be used to unsubscribe from Events | BUG | Major | ready | Yes | Yes |
| [S3246](https://rules.sonarsource.com/csharp/RSPEC-3246) | Generic type parameters should be co/contravariant when possible | CODE_SMELL | Major | ready | Yes | Yes |
| [S3247](https://rules.sonarsource.com/csharp/RSPEC-3247) | Duplicate casts should not be made | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3249](https://rules.sonarsource.com/csharp/RSPEC-3249) | Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals" | BUG | Major | ready | No | No |
| [S3251](https://rules.sonarsource.com/csharp/RSPEC-3251) | Implementations should be provided for "partial" methods | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3253](https://rules.sonarsource.com/csharp/RSPEC-3253) | Constructor and destructor declarations should not be redundant | CODE_SMELL | Minor | ready | No | No |
| [S3254](https://rules.sonarsource.com/csharp/RSPEC-3254) | Default parameter values should not be passed as arguments | CODE_SMELL | Minor | ready | No | No |
| [S3256](https://rules.sonarsource.com/csharp/RSPEC-3256) | "string.IsNullOrEmpty" should be used | CODE_SMELL | Minor | ready | No | No |
| [S3257](https://rules.sonarsource.com/csharp/RSPEC-3257) | Declarations and initializations should be as concise as possible | CODE_SMELL | Minor | ready | No | No |
| [S3260](https://rules.sonarsource.com/csharp/RSPEC-3260) | Non-derived "private" classes and records should be "sealed" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3261](https://rules.sonarsource.com/csharp/RSPEC-3261) | Namespaces should not be empty | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3262](https://rules.sonarsource.com/csharp/RSPEC-3262) | "params" should be used on overrides | CODE_SMELL | Major | ready | Yes | Yes |
| [S3263](https://rules.sonarsource.com/csharp/RSPEC-3263) | Static fields should appear in the order they must be initialized  | BUG | Major | ready | Yes | Yes |
| [S3264](https://rules.sonarsource.com/csharp/RSPEC-3264) | Events should be invoked | CODE_SMELL | Major | ready | Yes | Yes |
| [S3265](https://rules.sonarsource.com/csharp/RSPEC-3265) | Non-flags enums should not be used in bitwise operations | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3267](https://rules.sonarsource.com/csharp/RSPEC-3267) | Loops should be simplified with "LINQ" expressions | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3329](https://rules.sonarsource.com/csharp/RSPEC-3329) | Cipher Block Chaining IVs should be unpredictable | VULNERABILITY | Critical | ready | Yes | Yes |
| [S3330](https://rules.sonarsource.com/csharp/RSPEC-3330) | Cookies should have the "HttpOnly" flag | VULNERABILITY | Minor | ready | Yes | Yes |
| [S3343](https://rules.sonarsource.com/csharp/RSPEC-3343) | Caller information parameters should come at the end of the parameter list | BUG | Major | ready | Yes | Yes |
| [S3346](https://rules.sonarsource.com/csharp/RSPEC-3346) | Expressions used in "Debug.Assert" should not produce side effects | BUG | Major | ready | Yes | Yes |
| [S3353](https://rules.sonarsource.com/csharp/RSPEC-3353) | Unchanged variables should be marked as "const" | CODE_SMELL | Critical | ready | No | Yes |
| [S3358](https://rules.sonarsource.com/csharp/RSPEC-3358) | Ternary operators should not be nested | CODE_SMELL | Major | ready | Yes | Yes |
| [S3363](https://rules.sonarsource.com/csharp/RSPEC-3363) | Date and time should not be used as a type for primary keys | BUG | Minor | ready | Yes | Yes |
| [S3366](https://rules.sonarsource.com/csharp/RSPEC-3366) | "this" should not be exposed from constructors | CODE_SMELL | Major | ready | No | No |
| [S3376](https://rules.sonarsource.com/csharp/RSPEC-3376) | Attribute, EventArgs, and Exception type names should end with the type being extended | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3397](https://rules.sonarsource.com/csharp/RSPEC-3397) | "base.Equals" should not be used to check for reference equality in "Equals" if "base" is not "object" | BUG | Minor | ready | Yes | Yes |
| [S3398](https://rules.sonarsource.com/csharp/RSPEC-3398) | "private" methods called only by inner classes should be moved to those classes | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3400](https://rules.sonarsource.com/csharp/RSPEC-3400) | Methods should not return constants | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3415](https://rules.sonarsource.com/csharp/RSPEC-3415) | Assertion arguments should be passed in the correct order | CODE_SMELL | Major | ready | Yes | Yes |
| [S3416](https://rules.sonarsource.com/csharp/RSPEC-3416) | Loggers should be named for their enclosing types | CODE_SMELL | Minor | ready | No | No |
| [S3427](https://rules.sonarsource.com/csharp/RSPEC-3427) | Method overloads with default parameter values should not overlap | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3431](https://rules.sonarsource.com/csharp/RSPEC-3431) | "[ExpectedException]" should not be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S3433](https://rules.sonarsource.com/csharp/RSPEC-3433) | Test method signatures should be correct | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3440](https://rules.sonarsource.com/csharp/RSPEC-3440) | Variables should not be checked against the values they're about to be assigned | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3441](https://rules.sonarsource.com/csharp/RSPEC-3441) | Redundant property names should be omitted in anonymous classes | CODE_SMELL | Minor | ready | No | No |
| [S3442](https://rules.sonarsource.com/csharp/RSPEC-3442) | "abstract" classes should not have "public" constructors | CODE_SMELL | Major | ready | Yes | Yes |
| [S3443](https://rules.sonarsource.com/csharp/RSPEC-3443) | Type should not be examined on "System.Type" instances | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3444](https://rules.sonarsource.com/csharp/RSPEC-3444) | Interfaces should not simply inherit from base interfaces with colliding members | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3445](https://rules.sonarsource.com/csharp/RSPEC-3445) | Exceptions should not be explicitly rethrown | CODE_SMELL | Major | ready | Yes | Yes |
| [S3447](https://rules.sonarsource.com/csharp/RSPEC-3447) | "[Optional]" should not be used on "ref" or "out" parameters | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3449](https://rules.sonarsource.com/csharp/RSPEC-3449) | Right operands of shift operators should be integers | BUG | Critical | ready | Yes | Yes |
| [S3450](https://rules.sonarsource.com/csharp/RSPEC-3450) | Parameters with "[DefaultParameterValue]" attributes should also be marked "[Optional]" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3451](https://rules.sonarsource.com/csharp/RSPEC-3451) | "[DefaultValue]" should not be used when "[DefaultParameterValue]" is meant | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3453](https://rules.sonarsource.com/csharp/RSPEC-3453) | Classes should not have only "private" constructors | BUG | Major | ready | Yes | Yes |
| [S3456](https://rules.sonarsource.com/csharp/RSPEC-3456) | "string.ToCharArray()" and "ReadOnlySpan<T>.ToArray()" should not be called redundantly | BUG | Minor | ready | Yes | Yes |
| [S3457](https://rules.sonarsource.com/csharp/RSPEC-3457) | Composite format strings should be used correctly | CODE_SMELL | Major | ready | Yes | Yes |
| [S3458](https://rules.sonarsource.com/csharp/RSPEC-3458) | Empty "case" clauses that fall through to the "default" should be omitted | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3459](https://rules.sonarsource.com/csharp/RSPEC-3459) | Unassigned members should be removed | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3464](https://rules.sonarsource.com/csharp/RSPEC-3464) | Type inheritance should not be recursive | BUG | Blocker | ready | Yes | Yes |
| [S3466](https://rules.sonarsource.com/csharp/RSPEC-3466) | Optional parameters should be passed to "base" calls | BUG | Major | ready | Yes | Yes |
| [S3532](https://rules.sonarsource.com/csharp/RSPEC-3532) | Empty "default" clauses should be removed | CODE_SMELL | Minor | ready | No | No |
| [S3597](https://rules.sonarsource.com/csharp/RSPEC-3597) | "ServiceContract" and "OperationContract" attributes should be used together | CODE_SMELL | Major | ready | Yes | Yes |
| [S3598](https://rules.sonarsource.com/csharp/RSPEC-3598) | One-way "OperationContract" methods should have "void" return type | BUG | Major | ready | Yes | Yes |
| [S3600](https://rules.sonarsource.com/csharp/RSPEC-3600) | "params" should not be introduced on overrides | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3603](https://rules.sonarsource.com/csharp/RSPEC-3603) | Methods with "Pure" attribute should return a value  | BUG | Major | ready | Yes | Yes |
| [S3604](https://rules.sonarsource.com/csharp/RSPEC-3604) | Member initializer values should not be redundant | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3610](https://rules.sonarsource.com/csharp/RSPEC-3610) | Nullable type comparison should not be redundant | BUG | Major | ready | Yes | Yes |
| [S3626](https://rules.sonarsource.com/csharp/RSPEC-3626) | Jump statements should not be redundant | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3655](https://rules.sonarsource.com/csharp/RSPEC-3655) | Empty nullable value should not be accessed | BUG | Major | ready | Yes | Yes |
| [S3717](https://rules.sonarsource.com/csharp/RSPEC-3717) | Track use of "NotImplementedException" | CODE_SMELL | Minor | ready | No | No |
| [S3776](https://rules.sonarsource.com/csharp/RSPEC-3776) | Cognitive Complexity of methods should not be too high | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3869](https://rules.sonarsource.com/csharp/RSPEC-3869) | "SafeHandle.DangerousGetHandle" should not be called | BUG | Blocker | ready | Yes | Yes |
| [S3871](https://rules.sonarsource.com/csharp/RSPEC-3871) | Exception types should be "public" | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3872](https://rules.sonarsource.com/csharp/RSPEC-3872) | Parameter names should not duplicate the names of their methods | CODE_SMELL | Minor | ready | No | No |
| [S3874](https://rules.sonarsource.com/csharp/RSPEC-3874) | "out" and "ref" parameters should not be used | CODE_SMELL | Critical | ready | No | No |
| [S3875](https://rules.sonarsource.com/csharp/RSPEC-3875) | "operator==" should not be overloaded on reference types | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3876](https://rules.sonarsource.com/csharp/RSPEC-3876) | Strings or integral types should be used for indexers | CODE_SMELL | Minor | ready | No | No |
| [S3877](https://rules.sonarsource.com/csharp/RSPEC-3877) | Exceptions should not be thrown from unexpected methods | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S3878](https://rules.sonarsource.com/csharp/RSPEC-3878) | Arrays should not be created for params parameters | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3880](https://rules.sonarsource.com/csharp/RSPEC-3880) | Finalizers should not be empty | CODE_SMELL | Major | ready | No | No |
| [S3881](https://rules.sonarsource.com/csharp/RSPEC-3881) | "IDisposable" should be implemented correctly | CODE_SMELL | Major | ready | Yes | Yes |
| [S3884](https://rules.sonarsource.com/csharp/RSPEC-3884) | "CoSetProxyBlanket" and "CoInitializeSecurity" should not be used | VULNERABILITY | Blocker | deprecated | No | No |
| [S3885](https://rules.sonarsource.com/csharp/RSPEC-3885) | "Assembly.Load" should be used | CODE_SMELL | Major | ready | No | No |
| [S3887](https://rules.sonarsource.com/csharp/RSPEC-3887) | Mutable, non-private fields should not be "readonly" | BUG | Minor | ready | Yes | Yes |
| [S3889](https://rules.sonarsource.com/csharp/RSPEC-3889) | "Thread.Resume" and "Thread.Suspend" should not be used | BUG | Blocker | ready | Yes | Yes |
| [S3897](https://rules.sonarsource.com/csharp/RSPEC-3897) | Classes that provide "Equals(<T>)" should implement "IEquatable<T>" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3898](https://rules.sonarsource.com/csharp/RSPEC-3898) | Value types should implement "IEquatable<T>" | CODE_SMELL | Major | ready | No | No |
| [S3900](https://rules.sonarsource.com/csharp/RSPEC-3900) | Arguments of public methods should be validated against null | CODE_SMELL | Major | ready | No | No |
| [S3902](https://rules.sonarsource.com/csharp/RSPEC-3902) | "Assembly.GetExecutingAssembly" should not be called | CODE_SMELL | Major | ready | No | No |
| [S3903](https://rules.sonarsource.com/csharp/RSPEC-3903) | Types should be defined in named namespaces | BUG | Major | ready | Yes | Yes |
| [S3904](https://rules.sonarsource.com/csharp/RSPEC-3904) | Assemblies should have version information | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3906](https://rules.sonarsource.com/csharp/RSPEC-3906) | Event Handlers should have the correct signature | CODE_SMELL | Major | ready | No | No |
| [S3908](https://rules.sonarsource.com/csharp/RSPEC-3908) | Generic event handlers should be used | CODE_SMELL | Major | ready | No | No |
| [S3909](https://rules.sonarsource.com/csharp/RSPEC-3909) | Collections should implement the generic interface | CODE_SMELL | Major | ready | No | No |
| [S3923](https://rules.sonarsource.com/csharp/RSPEC-3923) | All branches in a conditional structure should not have exactly the same implementation | BUG | Major | ready | Yes | Yes |
| [S3925](https://rules.sonarsource.com/csharp/RSPEC-3925) | "ISerializable" should be implemented correctly | CODE_SMELL | Major | ready | Yes | Yes |
| [S3926](https://rules.sonarsource.com/csharp/RSPEC-3926) | Deserialization methods should be provided for "OptionalField" members | BUG | Major | ready | Yes | Yes |
| [S3927](https://rules.sonarsource.com/csharp/RSPEC-3927) | Serialization event handlers should be implemented correctly | BUG | Major | ready | Yes | Yes |
| [S3928](https://rules.sonarsource.com/csharp/RSPEC-3928) | Parameter names used into ArgumentException constructors should match an existing one  | CODE_SMELL | Major | ready | Yes | Yes |
| [S3937](https://rules.sonarsource.com/csharp/RSPEC-3937) | Number patterns should be regular | CODE_SMELL | Critical | ready | No | No |
| [S3949](https://rules.sonarsource.com/csharp/RSPEC-3949) | Calculations should not overflow | BUG | Major | ready | Yes | Yes |
| [S3956](https://rules.sonarsource.com/csharp/RSPEC-3956) | "Generic.List" instances should not be part of public APIs | CODE_SMELL | Major | ready | No | No |
| [S3962](https://rules.sonarsource.com/csharp/RSPEC-3962) | "static readonly" constants should be "const" instead | CODE_SMELL | Minor | ready | No | Yes |
| [S3963](https://rules.sonarsource.com/csharp/RSPEC-3963) | "static" fields should be initialized inline | CODE_SMELL | Minor | ready | Yes | Yes |
| [S3966](https://rules.sonarsource.com/csharp/RSPEC-3966) | Objects should not be disposed more than once | CODE_SMELL | Major | ready | Yes | Yes |
| [S3967](https://rules.sonarsource.com/csharp/RSPEC-3967) | Multidimensional arrays should not be used | CODE_SMELL | Minor | ready | No | No |
| [S3971](https://rules.sonarsource.com/csharp/RSPEC-3971) | "GC.SuppressFinalize" should not be called | CODE_SMELL | Major | ready | Yes | Yes |
| [S3972](https://rules.sonarsource.com/csharp/RSPEC-3972) | Conditionals should start on new lines | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3973](https://rules.sonarsource.com/csharp/RSPEC-3973) | A conditionally executed single line should be denoted by indentation | CODE_SMELL | Critical | ready | Yes | Yes |
| [S3981](https://rules.sonarsource.com/csharp/RSPEC-3981) | Collection sizes and array length comparisons should make sense | BUG | Major | ready | Yes | Yes |
| [S3984](https://rules.sonarsource.com/csharp/RSPEC-3984) | Exceptions should not be created without being thrown | BUG | Major | ready | Yes | Yes |
| [S3990](https://rules.sonarsource.com/csharp/RSPEC-3990) | Assemblies should be marked as CLS compliant | CODE_SMELL | Major | ready | No | No |
| [S3992](https://rules.sonarsource.com/csharp/RSPEC-3992) | Assemblies should explicitly specify COM visibility | CODE_SMELL | Major | ready | No | No |
| [S3993](https://rules.sonarsource.com/csharp/RSPEC-3993) | Custom attributes should be marked with "System.AttributeUsageAttribute" | CODE_SMELL | Major | ready | Yes | Yes |
| [S3994](https://rules.sonarsource.com/csharp/RSPEC-3994) | URI Parameters should not be strings | CODE_SMELL | Major | ready | No | No |
| [S3995](https://rules.sonarsource.com/csharp/RSPEC-3995) | URI return values should not be strings | CODE_SMELL | Major | ready | No | No |
| [S3996](https://rules.sonarsource.com/csharp/RSPEC-3996) | URI properties should not be strings | CODE_SMELL | Major | ready | No | No |
| [S3997](https://rules.sonarsource.com/csharp/RSPEC-3997) | String URI overloads should call "System.Uri" overloads | CODE_SMELL | Major | ready | No | No |
| [S3998](https://rules.sonarsource.com/csharp/RSPEC-3998) | Threads should not lock on objects with weak identity | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4000](https://rules.sonarsource.com/csharp/RSPEC-4000) | Pointers to unmanaged memory should not be visible | CODE_SMELL | Critical | ready | No | No |
| [S4002](https://rules.sonarsource.com/csharp/RSPEC-4002) | Disposable types should declare finalizers | CODE_SMELL | Major | ready | No | No |
| [S4004](https://rules.sonarsource.com/csharp/RSPEC-4004) | Collection properties should be readonly | CODE_SMELL | Major | ready | No | No |
| [S4005](https://rules.sonarsource.com/csharp/RSPEC-4005) | "System.Uri" arguments should be used instead of strings | CODE_SMELL | Major | ready | No | No |
| [S4015](https://rules.sonarsource.com/csharp/RSPEC-4015) | Inherited member visibility should not be decreased | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4016](https://rules.sonarsource.com/csharp/RSPEC-4016) | Enumeration members should not be named "Reserved" | CODE_SMELL | Major | ready | No | No |
| [S4017](https://rules.sonarsource.com/csharp/RSPEC-4017) | Method signatures should not contain nested generic types | CODE_SMELL | Major | ready | No | No |
| [S4018](https://rules.sonarsource.com/csharp/RSPEC-4018) | All type parameters should be used in the parameter list to enable type inference | CODE_SMELL | Minor | ready | No | No |
| [S4019](https://rules.sonarsource.com/csharp/RSPEC-4019) | Base class methods should not be hidden | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4022](https://rules.sonarsource.com/csharp/RSPEC-4022) | Enumerations should have "Int32" storage | CODE_SMELL | Minor | ready | No | No |
| [S4023](https://rules.sonarsource.com/csharp/RSPEC-4023) | Interfaces should not be empty | CODE_SMELL | Minor | ready | No | No |
| [S4025](https://rules.sonarsource.com/csharp/RSPEC-4025) | Child class fields should not differ from parent class fields only by capitalization | CODE_SMELL | Critical | ready | No | No |
| [S4026](https://rules.sonarsource.com/csharp/RSPEC-4026) | Assemblies should be marked with "NeutralResourcesLanguageAttribute" | CODE_SMELL | Minor | ready | No | No |
| [S4027](https://rules.sonarsource.com/csharp/RSPEC-4027) | Exceptions should provide standard constructors | CODE_SMELL | Minor | ready | No | No |
| [S4035](https://rules.sonarsource.com/csharp/RSPEC-4035) | Classes implementing "IEquatable<T>" should be sealed | CODE_SMELL | Major | ready | Yes | Yes |
| [S4036](https://rules.sonarsource.com/csharp/RSPEC-4036) | OS commands should not rely on PATH resolution | VULNERABILITY | Minor | ready | Yes | Yes |
| [S4039](https://rules.sonarsource.com/csharp/RSPEC-4039) | Interface methods should be callable by derived types | CODE_SMELL | Critical | ready | No | No |
| [S4040](https://rules.sonarsource.com/csharp/RSPEC-4040) | Strings should be normalized to uppercase | CODE_SMELL | Minor | ready | No | No |
| [S4041](https://rules.sonarsource.com/csharp/RSPEC-4041) | Type names should not match namespaces | CODE_SMELL | Minor | ready | No | No |
| [S4047](https://rules.sonarsource.com/csharp/RSPEC-4047) | Generics should be used when appropriate | CODE_SMELL | Minor | ready | No | No |
| [S4049](https://rules.sonarsource.com/csharp/RSPEC-4049) | Properties should be preferred | CODE_SMELL | Minor | ready | No | No |
| [S4050](https://rules.sonarsource.com/csharp/RSPEC-4050) | Operators should be overloaded consistently | CODE_SMELL | Major | ready | Yes | Yes |
| [S4052](https://rules.sonarsource.com/csharp/RSPEC-4052) | Types should not extend outdated base types | CODE_SMELL | Minor | ready | Yes | Yes |
| [S4055](https://rules.sonarsource.com/csharp/RSPEC-4055) | Literals should not be passed as localized parameters | CODE_SMELL | Major | ready | No | No |
| [S4056](https://rules.sonarsource.com/csharp/RSPEC-4056) | Overloads with a "CultureInfo" or an "IFormatProvider" parameter should be used | CODE_SMELL | Minor | ready | No | No |
| [S4057](https://rules.sonarsource.com/csharp/RSPEC-4057) | Locales should be set for data types | CODE_SMELL | Major | ready | No | No |
| [S4058](https://rules.sonarsource.com/csharp/RSPEC-4058) | Overloads with a "StringComparison" parameter should be used | CODE_SMELL | Minor | ready | No | No |
| [S4059](https://rules.sonarsource.com/csharp/RSPEC-4059) | Property names should not match get methods | CODE_SMELL | Major | ready | No | No |
| [S4060](https://rules.sonarsource.com/csharp/RSPEC-4060) | Non-abstract attributes should be sealed | CODE_SMELL | Minor | ready | No | No |
| [S4061](https://rules.sonarsource.com/csharp/RSPEC-4061) | "params" should be used instead of "varargs" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S4069](https://rules.sonarsource.com/csharp/RSPEC-4069) | Operator overloads should have named alternatives | CODE_SMELL | Minor | ready | No | No |
| [S4070](https://rules.sonarsource.com/csharp/RSPEC-4070) | Non-flags enums should not be marked with "FlagsAttribute" | CODE_SMELL | Major | ready | Yes | Yes |
| [S4136](https://rules.sonarsource.com/csharp/RSPEC-4136) | Method overloads should be grouped together | CODE_SMELL | Minor | ready | Yes | Yes |
| [S4143](https://rules.sonarsource.com/csharp/RSPEC-4143) | Collection elements should not be replaced unconditionally | BUG | Major | ready | Yes | Yes |
| [S4144](https://rules.sonarsource.com/csharp/RSPEC-4144) | Methods should not have identical implementations | CODE_SMELL | Major | ready | Yes | Yes |
| [S4158](https://rules.sonarsource.com/csharp/RSPEC-4158) | Empty collections should not be accessed or iterated | BUG | Minor | ready | Yes | Yes |
| [S4159](https://rules.sonarsource.com/csharp/RSPEC-4159) | Classes should implement their "ExportAttribute" interfaces | BUG | Blocker | ready | Yes | Yes |
| [S4200](https://rules.sonarsource.com/csharp/RSPEC-4200) | Native methods should be wrapped | CODE_SMELL | Major | ready | Yes | Yes |
| [S4201](https://rules.sonarsource.com/csharp/RSPEC-4201) | Null checks should not be combined with "is" operator checks | CODE_SMELL | Minor | ready | Yes | Yes |
| [S4210](https://rules.sonarsource.com/csharp/RSPEC-4210) | Windows Forms entry points should be marked with STAThread | BUG | Major | ready | Yes | Yes |
| [S4211](https://rules.sonarsource.com/csharp/RSPEC-4211) | Members should not have conflicting transparency annotations | VULNERABILITY | Major | ready | Yes | Yes |
| [S4212](https://rules.sonarsource.com/csharp/RSPEC-4212) | Serialization constructors should be secured | VULNERABILITY | Major | deprecated | No | No |
| [S4214](https://rules.sonarsource.com/csharp/RSPEC-4214) | "P/Invoke" methods should not be visible | CODE_SMELL | Major | deprecated | No | No |
| [S4220](https://rules.sonarsource.com/csharp/RSPEC-4220) | Events should have proper arguments | CODE_SMELL | Major | ready | Yes | Yes |
| [S4225](https://rules.sonarsource.com/csharp/RSPEC-4225) | Extension methods should not extend "object" | CODE_SMELL | Minor | ready | No | No |
| [S4226](https://rules.sonarsource.com/csharp/RSPEC-4226) | Extensions should be in separate namespaces | CODE_SMELL | Minor | ready | No | No |
| [S4260](https://rules.sonarsource.com/csharp/RSPEC-4260) | "ConstructorArgument" parameters should exist in constructors | BUG | Major | ready | Yes | Yes |
| [S4261](https://rules.sonarsource.com/csharp/RSPEC-4261) | Methods should be named according to their synchronicities | CODE_SMELL | Minor | ready | No | No |
| [S4275](https://rules.sonarsource.com/csharp/RSPEC-4275) | Getters and setters should access the expected fields | BUG | Critical | ready | Yes | Yes |
| [S4277](https://rules.sonarsource.com/csharp/RSPEC-4277) | "Shared" parts should not be created with "new" | BUG | Critical | ready | Yes | Yes |
| [S4347](https://rules.sonarsource.com/csharp/RSPEC-4347) | Secure random number generators should not output predictable values | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4423](https://rules.sonarsource.com/csharp/RSPEC-4423) | Weak SSL/TLS protocols should not be used | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4426](https://rules.sonarsource.com/csharp/RSPEC-4426) | Cryptographic keys should be robust | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4428](https://rules.sonarsource.com/csharp/RSPEC-4428) | "PartCreationPolicyAttribute" should be used with "ExportAttribute" | BUG | Major | ready | Yes | Yes |
| [S4433](https://rules.sonarsource.com/csharp/RSPEC-4433) | LDAP connections should be authenticated | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4456](https://rules.sonarsource.com/csharp/RSPEC-4456) | Parameter validation in yielding methods should be wrapped | CODE_SMELL | Major | ready | Yes | Yes |
| [S4457](https://rules.sonarsource.com/csharp/RSPEC-4457) | Parameter validation in "async"/"await" methods should be wrapped | CODE_SMELL | Major | ready | No | No |
| [S4462](https://rules.sonarsource.com/csharp/RSPEC-4462) | Calls to "async" methods should not be blocking | CODE_SMELL | Blocker | ready | No | Yes |
| [S4487](https://rules.sonarsource.com/csharp/RSPEC-4487) | Unread "private" fields should be removed | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4502](https://rules.sonarsource.com/csharp/RSPEC-4502) | CSRF protections should not be disabled | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4507](https://rules.sonarsource.com/csharp/RSPEC-4507) | Debugging features should not be enabled in production | VULNERABILITY | Minor | ready | Yes | Yes |
| [S4524](https://rules.sonarsource.com/csharp/RSPEC-4524) | "default" clauses should be first or last | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4545](https://rules.sonarsource.com/csharp/RSPEC-4545) | "DebuggerDisplayAttribute" strings should reference existing members | CODE_SMELL | Major | ready | Yes | Yes |
| [S4581](https://rules.sonarsource.com/csharp/RSPEC-4581) | "new Guid()" should not be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S4583](https://rules.sonarsource.com/csharp/RSPEC-4583) | Calls to delegate's method "BeginInvoke" should be paired with calls to "EndInvoke" | BUG | Critical | ready | Yes | Yes |
| [S4586](https://rules.sonarsource.com/csharp/RSPEC-4586) | Non-async "Task/Task<T>" methods should not return null | BUG | Critical | ready | Yes | Yes |
| [S4635](https://rules.sonarsource.com/csharp/RSPEC-4635) | Start index should be used instead of calling Substring | CODE_SMELL | Critical | ready | Yes | Yes |
| [S4663](https://rules.sonarsource.com/csharp/RSPEC-4663) | Comments should not be empty | CODE_SMELL | Minor | ready | Yes | Yes |
| [S4790](https://rules.sonarsource.com/csharp/RSPEC-4790) | Weak hashing algorithms should not be used | VULNERABILITY | Critical | ready | Yes | Yes |
| [S4830](https://rules.sonarsource.com/csharp/RSPEC-4830) | Server certificates should be verified during SSL/TLS connections | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5034](https://rules.sonarsource.com/csharp/RSPEC-5034) | "ValueTask" should be consumed correctly | CODE_SMELL | Critical | ready | Yes | Yes |
| [S5042](https://rules.sonarsource.com/csharp/RSPEC-5042) | Expanding archive files should not be done without controlling resource consumption | CODE_SMELL | Major | deprecated | No | No |
| [S5122](https://rules.sonarsource.com/csharp/RSPEC-5122) | Cross-Origin Resource Sharing (CORS) policy should be restricted to trusted origins | VULNERABILITY | Major | ready | Yes | Yes |
| [S5332](https://rules.sonarsource.com/csharp/RSPEC-5332) | Clear-text protocols should not be used | VULNERABILITY | Minor | ready | Yes | Yes |
| [S5344](https://rules.sonarsource.com/csharp/RSPEC-5344) | Passwords should not be stored in plaintext or with a fast hashing algorithm | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5443](https://rules.sonarsource.com/csharp/RSPEC-5443) | Temporary files should not be created in publicly writable directories | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5445](https://rules.sonarsource.com/csharp/RSPEC-5445) | Insecure temporary file creation methods should not be used | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5542](https://rules.sonarsource.com/csharp/RSPEC-5542) | Encryption algorithms should be used with secure mode and padding scheme | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5547](https://rules.sonarsource.com/csharp/RSPEC-5547) | Cipher algorithms should be robust | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5659](https://rules.sonarsource.com/csharp/RSPEC-5659) | JWT should be signed and verified with strong cipher algorithms | VULNERABILITY | Critical | ready | Yes | Yes |
| [S5693](https://rules.sonarsource.com/csharp/RSPEC-5693) | HTTP request content length should be limited | VULNERABILITY | Major | ready | Yes | Yes |
| [S5753](https://rules.sonarsource.com/csharp/RSPEC-5753) | ASP.NET Request Validation should not be disabled | VULNERABILITY | Major | ready | Yes | Yes |
| [S5766](https://rules.sonarsource.com/csharp/RSPEC-5766) | Serializable objects should validate data during deserialization | VULNERABILITY | Major | ready | Yes | Yes |
| [S5773](https://rules.sonarsource.com/csharp/RSPEC-5773) | Types allowed to be deserialized should be restricted | VULNERABILITY | Major | ready | Yes | Yes |
| [S5856](https://rules.sonarsource.com/csharp/RSPEC-5856) | Regular expressions should be syntactically valid | BUG | Critical | ready | Yes | Yes |
| [S6354](https://rules.sonarsource.com/csharp/RSPEC-6354) | Use a testable date/time provider | CODE_SMELL | Major | ready | No | No |
| [S6377](https://rules.sonarsource.com/csharp/RSPEC-6377) | XML signatures should be validated securely | VULNERABILITY | Major | ready | Yes | Yes |
| [S6418](https://rules.sonarsource.com/csharp/RSPEC-6418) | Secrets should not be hard-coded | VULNERABILITY | Blocker | ready | Yes | Yes |
| [S6419](https://rules.sonarsource.com/csharp/RSPEC-6419) | Azure Functions should be stateless | CODE_SMELL | Major | ready | Yes | Yes |
| [S6420](https://rules.sonarsource.com/csharp/RSPEC-6420) | Client instances should not be recreated on each Azure Function invocation | CODE_SMELL | Major | ready | Yes | Yes |
| [S6421](https://rules.sonarsource.com/csharp/RSPEC-6421) | Azure Functions should use Structured Error Handling | CODE_SMELL | Major | ready | No | No |
| [S6422](https://rules.sonarsource.com/csharp/RSPEC-6422) | Calls to "async" methods should not be blocking in Azure Functions | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S6423](https://rules.sonarsource.com/csharp/RSPEC-6423) | Azure Functions should log all failures | CODE_SMELL | Major | ready | No | No |
| [S6424](https://rules.sonarsource.com/csharp/RSPEC-6424) | Interfaces for durable entities should satisfy the restrictions | CODE_SMELL | Blocker | ready | Yes | Yes |
| [S6444](https://rules.sonarsource.com/csharp/RSPEC-6444) | Regular expressions should be executed with a timeout | VULNERABILITY | Minor | ready | Yes | Yes |
| [S6507](https://rules.sonarsource.com/csharp/RSPEC-6507) | Blocks should not be synchronized on local variables | BUG | Major | ready | No | No |
| [S6513](https://rules.sonarsource.com/csharp/RSPEC-6513) | "ExcludeFromCodeCoverage" attributes should include a justification | CODE_SMELL | Minor | ready | No | No |
| [S6561](https://rules.sonarsource.com/csharp/RSPEC-6561) | Avoid using "DateTime.Now" for benchmarking or timing operations | CODE_SMELL | Major | ready | Yes | Yes |
| [S6562](https://rules.sonarsource.com/csharp/RSPEC-6562) | Always set the "DateTimeKind" when creating new "DateTime" instances | CODE_SMELL | Major | ready | Yes | Yes |
| [S6563](https://rules.sonarsource.com/csharp/RSPEC-6563) | Use UTC when recording DateTime instants | CODE_SMELL | Major | ready | No | No |
| [S6566](https://rules.sonarsource.com/csharp/RSPEC-6566) | Use "DateTimeOffset" instead of "DateTime" | CODE_SMELL | Major | ready | No | No |
| [S6575](https://rules.sonarsource.com/csharp/RSPEC-6575) | Use "TimeZoneInfo.FindSystemTimeZoneById" without converting the timezones with "TimezoneConverter" | CODE_SMELL | Major | ready | Yes | Yes |
| [S6580](https://rules.sonarsource.com/csharp/RSPEC-6580) | Use a format provider when parsing date and time | CODE_SMELL | Major | ready | Yes | Yes |
| [S6585](https://rules.sonarsource.com/csharp/RSPEC-6585) | Don't hardcode the format when turning dates and times to strings | CODE_SMELL | Minor | ready | No | No |
| [S6588](https://rules.sonarsource.com/csharp/RSPEC-6588) | Use the "UnixEpoch" field instead of creating "DateTime" instances that point to the beginning of the Unix epoch | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6602](https://rules.sonarsource.com/csharp/RSPEC-6602) | "Find" method should be used instead of the "FirstOrDefault" extension | CODE_SMELL | Minor | ready | No | Yes |
| [S6603](https://rules.sonarsource.com/csharp/RSPEC-6603) | The collection-specific "TrueForAll" method should be used instead of the "All" extension | CODE_SMELL | Minor | ready | No | Yes |
| [S6605](https://rules.sonarsource.com/csharp/RSPEC-6605) | Collection-specific "Exists" method should be used instead of the "Any" extension | CODE_SMELL | Minor | ready | No | Yes |
| [S6607](https://rules.sonarsource.com/csharp/RSPEC-6607) | The collection should be filtered before sorting by using "Where" before "OrderBy" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6608](https://rules.sonarsource.com/csharp/RSPEC-6608) | Prefer indexing instead of "Enumerable" methods on types implementing "IList" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6609](https://rules.sonarsource.com/csharp/RSPEC-6609) | "Min/Max" properties of "Set" types should be used instead of the "Enumerable" extension methods | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6610](https://rules.sonarsource.com/csharp/RSPEC-6610) | "StartsWith" and "EndsWith" overloads that take a "char" should be used instead of the ones that take a "string" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6612](https://rules.sonarsource.com/csharp/RSPEC-6612) | The lambda parameter should be used instead of capturing arguments in "ConcurrentDictionary" methods | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6613](https://rules.sonarsource.com/csharp/RSPEC-6613) | "First" and "Last" properties of "LinkedList" should be used instead of the "First()" and "Last()" extension methods | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6617](https://rules.sonarsource.com/csharp/RSPEC-6617) | "Contains" should be used instead of "Any" for simple equality checks | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6618](https://rules.sonarsource.com/csharp/RSPEC-6618) | "string.Create" should be used instead of "FormattableString" | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6640](https://rules.sonarsource.com/csharp/RSPEC-6640) | Unsafe code blocks should not be used | VULNERABILITY | Major | ready | Yes | Yes |
| [S6664](https://rules.sonarsource.com/csharp/RSPEC-6664) | The code block contains too many logging calls | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6667](https://rules.sonarsource.com/csharp/RSPEC-6667) | Logging in a catch clause should pass the caught exception as a parameter. | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6668](https://rules.sonarsource.com/csharp/RSPEC-6668) | Logging arguments should be passed to the correct parameter | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6669](https://rules.sonarsource.com/csharp/RSPEC-6669) | Logger field or property name should comply with a naming convention | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6670](https://rules.sonarsource.com/csharp/RSPEC-6670) | "Trace.Write" and "Trace.WriteLine" should not be used | CODE_SMELL | Minor | ready | No | No |
| [S6672](https://rules.sonarsource.com/csharp/RSPEC-6672) | Generic logger injection should match enclosing type | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6673](https://rules.sonarsource.com/csharp/RSPEC-6673) | Log message template placeholders should be in the right order | CODE_SMELL | Major | ready | Yes | Yes |
| [S6674](https://rules.sonarsource.com/csharp/RSPEC-6674) | Log message template should be syntactically correct | BUG | Critical | ready | Yes | Yes |
| [S6675](https://rules.sonarsource.com/csharp/RSPEC-6675) | "Trace.WriteLineIf" should not be used with "TraceSwitch" levels | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6677](https://rules.sonarsource.com/csharp/RSPEC-6677) | Message template placeholders should be unique | BUG | Major | ready | Yes | Yes |
| [S6678](https://rules.sonarsource.com/csharp/RSPEC-6678) | Use PascalCase for named placeholders | CODE_SMELL | Minor | ready | Yes | Yes |
| [S6781](https://rules.sonarsource.com/csharp/RSPEC-6781) | JWT secret keys should not be disclosed | VULNERABILITY | Blocker | ready | Yes | Yes |
| [S6797](https://rules.sonarsource.com/csharp/RSPEC-6797) | Blazor query parameter type should be supported | BUG | Major | ready | Yes | Yes |
| [S6798](https://rules.sonarsource.com/csharp/RSPEC-6798) | [JSInvokable] attribute should only be used on public methods | BUG | Major | ready | Yes | Yes |
| [S6800](https://rules.sonarsource.com/csharp/RSPEC-6800) | Component parameter type should match the route parameter type constraint | BUG | Major | ready | Yes | Yes |
| [S6802](https://rules.sonarsource.com/csharp/RSPEC-6802) | Using lambda expressions in loops should be avoided in Blazor markup section | CODE_SMELL | Major | ready | No | Yes |
| [S6803](https://rules.sonarsource.com/csharp/RSPEC-6803) | Parameters with SupplyParameterFromQuery attribute should be used only in routable components | CODE_SMELL | Major | deprecated | No | No |
| [S6930](https://rules.sonarsource.com/csharp/RSPEC-6930) | Backslash should be avoided in route templates | BUG | Major | ready | Yes | Yes |
| [S6931](https://rules.sonarsource.com/csharp/RSPEC-6931) | ASP.NET controller actions should not have a route template starting with "/" | CODE_SMELL | Major | ready | Yes | Yes |
| [S6932](https://rules.sonarsource.com/csharp/RSPEC-6932) | Use model binding instead of reading raw request data | CODE_SMELL | Major | ready | Yes | Yes |
| [S6934](https://rules.sonarsource.com/csharp/RSPEC-6934) | A Route attribute should be added to the controller when a route template is specified at the action level | CODE_SMELL | Major | ready | Yes | Yes |
| [S6960](https://rules.sonarsource.com/csharp/RSPEC-6960) | Controllers should not have mixed responsibilities | CODE_SMELL | Major | ready | Yes | Yes |
| [S6961](https://rules.sonarsource.com/csharp/RSPEC-6961) | API Controllers should derive from ControllerBase instead of Controller | CODE_SMELL | Major | ready | Yes | Yes |
| [S6962](https://rules.sonarsource.com/csharp/RSPEC-6962) | You should pool HTTP connections with HttpClientFactory | CODE_SMELL | Major | ready | Yes | Yes |
| [S6964](https://rules.sonarsource.com/csharp/RSPEC-6964) | Value type property used as input in a controller action should be nullable, required or annotated with the JsonRequiredAttribute to avoid under-posting. | CODE_SMELL | Major | ready | Yes | Yes |
| [S6965](https://rules.sonarsource.com/csharp/RSPEC-6965) | REST API actions should be annotated with an HTTP verb attribute | CODE_SMELL | Major | ready | Yes | Yes |
| [S6966](https://rules.sonarsource.com/csharp/RSPEC-6966) | Awaitable method should be used | CODE_SMELL | Major | ready | Yes | Yes |
| [S6967](https://rules.sonarsource.com/csharp/RSPEC-6967) | ModelState.IsValid should be called in controller actions | CODE_SMELL | Critical | ready | Yes | Yes |
| [S6968](https://rules.sonarsource.com/csharp/RSPEC-6968) | Actions that return a value should be annotated with ProducesResponseTypeAttribute containing the return type | CODE_SMELL | Major | ready | Yes | Yes |
| [S7039](https://rules.sonarsource.com/csharp/RSPEC-7039) | Content Security Policies should be restrictive | VULNERABILITY | Major | ready | Yes | Yes |
| [S7130](https://rules.sonarsource.com/csharp/RSPEC-7130) | First/Single should be used instead of FirstOrDefault/SingleOrDefault on collections that are known to be non-empty | CODE_SMELL | Major | ready | Yes | Yes |
| [S7131](https://rules.sonarsource.com/csharp/RSPEC-7131) | A write lock should not be released when a read lock has been acquired and vice versa | BUG | Critical | ready | Yes | Yes |
| [S7133](https://rules.sonarsource.com/csharp/RSPEC-7133) | Locks should be released within the same method | BUG | Critical | ready | Yes | Yes |
| [S8367](https://rules.sonarsource.com/csharp/RSPEC-8367) | Identifiers should not conflict with the C# 14 "field" contextual keyword | CODE_SMELL | Critical | ready | Yes | Yes |
| [S8368](https://rules.sonarsource.com/csharp/RSPEC-8368) | Identifiers should not conflict with the C# 14 "extension" contextual keyword | CODE_SMELL | Critical | ready | Yes | Yes |
| [S8380](https://rules.sonarsource.com/csharp/RSPEC-8380) | Return types named "partial" should be escaped with "@" | CODE_SMELL | Critical | ready | Yes | Yes |
| [S8381](https://rules.sonarsource.com/csharp/RSPEC-8381) | "scoped" should be escaped when used as an identifier or type name in parenthesized lambda parameter lists | CODE_SMELL | Critical | ready | Yes | Yes |
| [S8717](https://rules.sonarsource.com/csharp/RSPEC-8717) | Multiple "[Key]" attributes should not be used to define a composite key | BUG | Blocker | ready | Yes | Yes |
| [S8718](https://rules.sonarsource.com/csharp/RSPEC-8718) | Client-evaluated default values should use database functions | BUG | Blocker | ready | Yes | Yes |
| [S8733](https://rules.sonarsource.com/csharp/RSPEC-8733) | Potential Cartesian Explosion | BUG | Major | ready | Yes | Yes |
| [S8747](https://rules.sonarsource.com/csharp/RSPEC-8747) | Migrations should not narrow column types without converting existing data | BUG | Major | ready | Yes | Yes |
| [S8949](https://rules.sonarsource.com/csharp/RSPEC-8949) | The overload accepting a 'CancellationToken' should be used | BUG | Major | ready | Yes | Yes |
| [S8969](https://rules.sonarsource.com/csharp/RSPEC-8969) | Null-forgiving operators should not be redundant | CODE_SMELL | Minor | ready | Yes | Yes |
| [S8970](https://rules.sonarsource.com/csharp/RSPEC-8970) | Null-forgiving operators should not be used when nullable warnings are disabled | CODE_SMELL | Minor | ready | Yes | Yes |
| [S9022](https://rules.sonarsource.com/csharp/RSPEC-9022) | Redundant "Include" calls should be removed | CODE_SMELL | Minor | ready | Yes | Yes |
| [S9023](https://rules.sonarsource.com/csharp/RSPEC-9023) | "Include" calls discarded by query reshaping should be fixed | BUG | Critical | ready | Yes | Yes |
| [S9118](https://rules.sonarsource.com/csharp/RSPEC-9118) | Default values should be compatible with their entity property type | BUG | Major | ready | Yes | Yes |
| [S9129](https://rules.sonarsource.com/csharp/RSPEC-9129) | "Include" and "ThenInclude" chains of reference navigations should be merged into a single "Include" call | CODE_SMELL | Minor | ready | Yes | Yes |
| [S9414](https://rules.sonarsource.com/csharp/RSPEC-9414) | Local variable declarations and assignments should be joined | CODE_SMELL | Minor | ready | Yes | Yes |
| [S9415](https://rules.sonarsource.com/csharp/RSPEC-9415) | Local functions should be marked "static" when they don't capture variables | CODE_SMELL | Major | ready | Yes | Yes |
