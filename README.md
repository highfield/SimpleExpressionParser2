# SimpleExpressionParser2
Simple expression parser/solver


## License
MIT license: https://opensource.org/licenses/MIT


## Sample output

Input expression: `3+5`

Tidy-up format: `3 + 5`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="3" />
  <XTokenNumber data="5" />
</XTokenOperAdd>
```
Evaluate to: `8`
***

Input expression: `3+5 +10`

Tidy-up format: `3 + (5 + 10)`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="3" />
  <XTokenOperAdd>
    <XTokenNumber data="5" />
    <XTokenNumber data="10" />
  </XTokenOperAdd>
</XTokenOperAdd>
```
Evaluate to: `18`
***

Input expression: `3+(5+1) + (2+3) +4`

Tidy-up format: `3 + ((5 + 1) + ((2 + 3) + 4))`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="3" />
  <XTokenOperAdd>
    <XTokenOperAdd>
      <XTokenNumber data="5" />
      <XTokenNumber data="1" />
    </XTokenOperAdd>
    <XTokenOperAdd>
      <XTokenOperAdd>
        <XTokenNumber data="2" />
        <XTokenNumber data="3" />
      </XTokenOperAdd>
      <XTokenNumber data="4" />
    </XTokenOperAdd>
  </XTokenOperAdd>
</XTokenOperAdd>
```
Evaluate to: `18`
***

Input expression: ` +7`

Tidy-up format: `+7`

Serialized tree:
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="7" />
</XTokenOperUnaryPlus>
```
Evaluate to: `7`
***

Input expression: ` + (4)`

Tidy-up format: `+4`

Serialized tree:
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="4" />
</XTokenOperUnaryPlus>
```
Evaluate to: `4`
***

Input expression: ` (+ +8)`

Tidy-up format: `++8`

Serialized tree:
```
<XTokenOperUnaryPlus>
  <XTokenOperUnaryPlus>
    <XTokenNumber data="8" />
  </XTokenOperUnaryPlus>
</XTokenOperUnaryPlus>
```
Evaluate to: `8`
***

Input expression: `(+9) == + +9`

Tidy-up format: `+9 == ++9`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenOperUnaryPlus>
    <XTokenNumber data="9" />
  </XTokenOperUnaryPlus>
  <XTokenOperUnaryPlus>
    <XTokenOperUnaryPlus>
      <XTokenNumber data="9" />
    </XTokenOperUnaryPlus>
  </XTokenOperUnaryPlus>
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: `3==+3`

Tidy-up format: `3 == +3`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenNumber data="3" />
  <XTokenOperUnaryPlus>
    <XTokenNumber data="3" />
  </XTokenOperUnaryPlus>
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: `3==--3`

Tidy-up format: `3 == --3`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenNumber data="3" />
  <XTokenOperUnaryMinus>
    <XTokenOperUnaryMinus>
      <XTokenNumber data="3" />
    </XTokenOperUnaryMinus>
  </XTokenOperUnaryMinus>
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: `-(1-5)+(2+7)-10`

Tidy-up format: `-(1 - 5) + ((2 + 7) - 10)`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenOperUnaryMinus>
    <XTokenOperSub>
      <XTokenNumber data="1" />
      <XTokenNumber data="5" />
    </XTokenOperSub>
  </XTokenOperUnaryMinus>
  <XTokenOperSub>
    <XTokenOperAdd>
      <XTokenNumber data="2" />
      <XTokenNumber data="7" />
    </XTokenOperAdd>
    <XTokenNumber data="10" />
  </XTokenOperSub>
</XTokenOperAdd>
```
Evaluate to: `3`
***

Input expression: `-10*2+(-2)*5/2`

Tidy-up format: `(-10 * 2) + (-2 * (5 / 2))`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenOperMul>
    <XTokenOperUnaryMinus>
      <XTokenNumber data="10" />
    </XTokenOperUnaryMinus>
    <XTokenNumber data="2" />
  </XTokenOperMul>
  <XTokenOperMul>
    <XTokenOperUnaryMinus>
      <XTokenNumber data="2" />
    </XTokenOperUnaryMinus>
    <XTokenOperDiv>
      <XTokenNumber data="5" />
      <XTokenNumber data="2" />
    </XTokenOperDiv>
  </XTokenOperMul>
</XTokenOperAdd>
```
Evaluate to: `-25`
***

Input expression: `1+0 / 0`

Tidy-up format: `1 + (0 / 0)`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="1" />
  <XTokenOperDiv>
    <XTokenNumber data="0" />
    <XTokenNumber data="0" />
  </XTokenOperDiv>
</XTokenOperAdd>
```
Evaluate to: `NaN`
***

Input expression: `1+13 % 10`

Tidy-up format: `1 + (13 % 10)`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="1" />
  <XTokenOperMod>
    <XTokenNumber data="13" />
    <XTokenNumber data="10" />
  </XTokenOperMod>
</XTokenOperAdd>
```
Evaluate to: `4`
***

Input expression: `2+3*5`

Tidy-up format: `2 + (3 * 5)`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenNumber data="2" />
  <XTokenOperMul>
    <XTokenNumber data="3" />
    <XTokenNumber data="5" />
  </XTokenOperMul>
</XTokenOperAdd>
```
Evaluate to: `17`
***

Input expression: `2*3-5`

Tidy-up format: `(2 * 3) - 5`

Serialized tree:
```
<XTokenOperSub>
  <XTokenOperMul>
    <XTokenNumber data="2" />
    <XTokenNumber data="3" />
  </XTokenOperMul>
  <XTokenNumber data="5" />
</XTokenOperSub>
```
Evaluate to: `1`
***

Input expression: `(10 & 2) == 2`

Tidy-up format: `(10 & 2) == 2`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenOperBitwiseAnd>
    <XTokenNumber data="10" />
    <XTokenNumber data="2" />
  </XTokenOperBitwiseAnd>
  <XTokenNumber data="2" />
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: `12345 & 56`

Tidy-up format: `12345 & 56`

Serialized tree:
```
<XTokenOperBitwiseAnd>
  <XTokenNumber data="12345" />
  <XTokenNumber data="56" />
</XTokenOperBitwiseAnd>
```
Evaluate to: `56`
***

Input expression: `(23 | 11) ^ ~66`

Tidy-up format: `(23 | 11) ^ ~66`

Serialized tree:
```
<XTokenOperBitwiseXor>
  <XTokenOperBitwiseOr>
    <XTokenNumber data="23" />
    <XTokenNumber data="11" />
  </XTokenOperBitwiseOr>
  <XTokenOperBitwiseNot>
    <XTokenNumber data="66" />
  </XTokenOperBitwiseNot>
</XTokenOperBitwiseXor>
```
Evaluate to: `-94`
***

Input expression: ``

Test error: **Empty expression.**
***

Input expression: `    `

Test error: **Empty expression.**
***

Input expression: `false  `

Tidy-up format: `false`

Serialized tree:
```
<XTokenBoolean data="false" />
```
Evaluate to: `False`
***

Input expression: `  true`

Tidy-up format: `true`

Serialized tree:
```
<XTokenBoolean data="true" />
```
Evaluate to: `True`
***

Input expression: `  null  `

Tidy-up format: `null`

Serialized tree:
```
<XTokenNull />
```
Evaluate to: ``
***

Input expression: ` 123 `

Tidy-up format: `123`

Serialized tree:
```
<XTokenNumber data="123" />
```
Evaluate to: `123`
***

Input expression: `-456  `

Tidy-up format: `-456`

Serialized tree:
```
<XTokenOperUnaryMinus>
  <XTokenNumber data="456" />
</XTokenOperUnaryMinus>
```
Evaluate to: `-456`
***

Input expression: ` +7 `

Tidy-up format: `+7`

Serialized tree:
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="7" />
</XTokenOperUnaryPlus>
```
Evaluate to: `7`
***

Input expression: ` 3.151927 `

Tidy-up format: `3.151927`

Serialized tree:
```
<XTokenNumber data="3.151927" />
```
Evaluate to: `3,151927`
***

Input expression: ` +2.718 `

Tidy-up format: `+2.718`

Serialized tree:
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="2.718" />
</XTokenOperUnaryPlus>
```
Evaluate to: `2,718`
***

Input expression: ` .5 `

Test error: **Illegal character found: .**
***

Input expression: ` -.5 `

Test error: **Illegal character found: .**
***

Input expression: ` +5. `

Test error: **Illegal character found:  **
***

Input expression: `myvar  `

Tidy-up format: `myvar`

Serialized tree:
```
<XTokenRefId data="myvar" />
```
Evaluate to: `MyVar`
***

Input expression: `  w0rd`

Tidy-up format: `w0rd`

Serialized tree:
```
<XTokenRefId data="w0rd" />
```
Evaluate to: ``
***

Input expression: `_abc_def_`

Tidy-up format: `_abc_def_`

Serialized tree:
```
<XTokenRefId data="_abc_def_" />
```
Evaluate to: `3,141592653589793`
***

Input expression: `my.multi.level.reference`

Tidy-up format: `my.multi.level.reference`

Serialized tree:
```
<XTokenRefId data="my.multi.level.reference" />
```
Evaluate to: ``
***

Input expression: ` 'incomplete string`

Test error: **Unexpected end of string.**
***

Input expression: ` '`

Test error: **Unexpected end of string.**
***

Input expression: ` ''`

Tidy-up format: `""`

Serialized tree:
```
<XTokenString data="" />
```
Evaluate to: ``
***

Input expression: `'' `

Tidy-up format: `""`

Serialized tree:
```
<XTokenString data="" />
```
Evaluate to: ``
***

Input expression: `'single-quoted string'`

Tidy-up format: `"single-quoted string"`

Serialized tree:
```
<XTokenString data="single-quoted string" />
```
Evaluate to: `single-quoted string`
***

Input expression: `"double-quoted string"`

Tidy-up format: `"double-quoted string"`

Serialized tree:
```
<XTokenString data="double-quoted string" />
```
Evaluate to: `double-quoted string`
***

Input expression: `'here is a "nested string"'`

Tidy-up format: `"here is a "nested string""`

Serialized tree:
```
<XTokenString data="here is a &quot;nested string&quot;" />
```
Evaluate to: `here is a "nested string"`
Verify error: **Missing operator.**
***

Input expression: `'here is a "nested escaped string"'`

Tidy-up format: `"here is a "nested escaped string""`

Serialized tree:
```
<XTokenString data="here is a &quot;nested escaped string&quot;" />
```
Evaluate to: `here is a "nested escaped string"`
Verify error: **Missing operator.**
***

Input expression: ` #  `

Test error: **Unexpected end of ISO8601 sequence.**
***

Input expression: ` #`

Test error: **Unexpected end of ISO8601 sequence.**
***

Input expression: ` ##  `

Test error: **Unsupported ISO8601 format: **
***

Input expression: ` # #`

Test error: **Unsupported ISO8601 format:  **
***

Input expression: ` #à #  `

Test error: **Unsupported ISO8601 format: à **
***

Input expression: ` #2007-04-05ù12:30-02:00#  `

Test error: **The string '2007-04-05ù12:30-02:00' was not recognized as a valid DateTime. There is an unknown word starting at index '10'.**
***

Input expression: ` #2007-03-01T13:00:00Z# `

Tidy-up format: `#2007-03-01T13:00:00.0000000Z#`

Serialized tree:
```
<XTokenDateTime data="2007-03-01T13:00:00Z" />
```
Evaluate to: `01/03/2007 13:00:00`
***

Input expression: ` #2007-04-05T12:30-02:00#  `

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenDateTime data="2007-04-05T16:30:00+02:00" />
```
Evaluate to: `05/04/2007 16:30:00`
***

Input expression: ` #2007-04-05T12:30-02:00#== #2007-04-05T12:30-02:00# `

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00# == #2007-04-05T16:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: ` #2007-04-05T12:30-02:00# < #2007-04-05T12:31-02:00#`

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00# < #2007-04-05T16:31:00.0000000+02:00#`

Serialized tree:
```
<XTokenOperLessThan>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T16:31:00+02:00" />
</XTokenOperLessThan>
```
Evaluate to: `True`
***

Input expression: ` #2007-04-05T12:30-02:00# != 123 `

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00# != 123`

Serialized tree:
```
<XTokenOperNotEqual>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenNumber data="123" />
</XTokenOperNotEqual>
```
Test error: **Cannot convert a DateTime to a Double.**
***

Input expression: ` #2007-04-05T12:30-02:00# + 123 `

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00# + 123`

Serialized tree:
```
<XTokenOperAdd>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenNumber data="123" />
</XTokenOperAdd>
```
Test error: **Cannot convert a DateTime to a Double.**
***

Input expression: ` #2007-04-05T12:30-02:00# #2007-04-05T12:30-02:00# `

Test error: **Missing operator.**
***

Input expression: ` #2007-04-05T12:30-02:00##2007-04-05T12:30-02:00# `

Test error: **Missing operator.**
***

Input expression: ` (#2007-04-05T12:30-02:00#)`

Tidy-up format: `#2007-04-05T16:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenDateTime data="2007-04-05T16:30:00+02:00" />
```
Evaluate to: `05/04/2007 16:30:00`
***

Input expression: ` #2007-04-05T12:30+02:00# == #2007-04-05T08:30:00-02:00#`

Tidy-up format: `#2007-04-05T12:30:00.0000000+02:00# == #2007-04-05T12:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: ` #2007-04-05T12:30+02:00# == #2007-04-05T10:30:00Z#`

Tidy-up format: `#2007-04-05T12:30:00.0000000+02:00# == #2007-04-05T10:30:00.0000000Z#`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T10:30:00Z" />
</XTokenOperEqual>
```
Evaluate to: `False`
***

Input expression: ` #3000-04-05T12:30+02:00#`

Tidy-up format: `#3000-04-05T12:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenDateTime data="3000-04-05T12:30:00+02:00" />
```
Evaluate to: `05/04/3000 12:30:00`
***

Input expression: ` #2018-04-05T12:30:45.123456789+02:00#`

Tidy-up format: `#2018-04-05T12:30:45.1234568+02:00#`

Serialized tree:
```
<XTokenDateTime data="2018-04-05T12:30:45.1234568+02:00" />
```
Evaluate to: `05/04/2018 12:30:45`
***

Input expression: ` #2007-04-05T12:30:45.123+02:00# > #2007-04-05T08:30:00-02:00#`

Tidy-up format: `#2007-04-05T12:30:45.1230000+02:00# > #2007-04-05T12:30:00.0000000+02:00#`

Serialized tree:
```
<XTokenOperGreaterThan>
  <XTokenDateTime data="2007-04-05T12:30:45.123+02:00" />
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
</XTokenOperGreaterThan>
```
Evaluate to: `True`
***

Input expression: `zero == zero  `

Tidy-up format: `zero == zero`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenRefId data="zero" />
  <XTokenRefId data="zero" />
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: ` black != white`

Tidy-up format: `black != white`

Serialized tree:
```
<XTokenOperNotEqual>
  <XTokenRefId data="black" />
  <XTokenRefId data="white" />
</XTokenOperNotEqual>
```
Evaluate to: `True`
***

Input expression: ` 12 < 45`

Tidy-up format: `12 < 45`

Serialized tree:
```
<XTokenOperLessThan>
  <XTokenNumber data="12" />
  <XTokenNumber data="45" />
</XTokenOperLessThan>
```
Evaluate to: `True`
***

Input expression: `20 >4`

Tidy-up format: `20 > 4`

Serialized tree:
```
<XTokenOperGreaterThan>
  <XTokenNumber data="20" />
  <XTokenNumber data="4" />
</XTokenOperGreaterThan>
```
Evaluate to: `True`
***

Input expression: `10<=100`

Tidy-up format: `10 <= 100`

Serialized tree:
```
<XTokenOperLessOrEqualThan>
  <XTokenNumber data="10" />
  <XTokenNumber data="100" />
</XTokenOperLessOrEqualThan>
```
Evaluate to: `True`
***

Input expression: `100   >=   1`

Tidy-up format: `100 >= 1`

Serialized tree:
```
<XTokenOperGreaterOrEqualThan>
  <XTokenNumber data="100" />
  <XTokenNumber data="1" />
</XTokenOperGreaterOrEqualThan>
```
Evaluate to: `True`
***

Input expression: `true && (1 < 2)`

Tidy-up format: `true && (1 < 2)`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenBoolean data="true" />
  <XTokenOperLessThan>
    <XTokenNumber data="1" />
    <XTokenNumber data="2" />
  </XTokenOperLessThan>
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `(3 < 5) && true`

Tidy-up format: `(3 < 5) && true`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenOperLessThan>
    <XTokenNumber data="3" />
    <XTokenNumber data="5" />
  </XTokenOperLessThan>
  <XTokenBoolean data="true" />
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `true && true`

Tidy-up format: `true && true`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenBoolean data="true" />
  <XTokenBoolean data="true" />
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `false || (true) || (1 == 2)`

Tidy-up format: `false || (true || (1 == 2))`

Serialized tree:
```
<XTokenOperLogicalOr>
  <XTokenBoolean data="false" />
  <XTokenOperLogicalOr>
    <XTokenBoolean data="true" />
    <XTokenOperEqual>
      <XTokenNumber data="1" />
      <XTokenNumber data="2" />
    </XTokenOperEqual>
  </XTokenOperLogicalOr>
</XTokenOperLogicalOr>
```
Evaluate to: `True`
***

Input expression: `(1==1) && (2==2) && true`

Tidy-up format: `(1 == 1) && ((2 == 2) && true)`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenOperEqual>
    <XTokenNumber data="1" />
    <XTokenNumber data="1" />
  </XTokenOperEqual>
  <XTokenOperLogicalAnd>
    <XTokenOperEqual>
      <XTokenNumber data="2" />
      <XTokenNumber data="2" />
    </XTokenOperEqual>
    <XTokenBoolean data="true" />
  </XTokenOperLogicalAnd>
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `!false==!!true`

Tidy-up format: `!false == !!true`

Serialized tree:
```
<XTokenOperEqual>
  <XTokenOperLogicalNot>
    <XTokenBoolean data="false" />
  </XTokenOperLogicalNot>
  <XTokenOperLogicalNot>
    <XTokenOperLogicalNot>
      <XTokenBoolean data="true" />
    </XTokenOperLogicalNot>
  </XTokenOperLogicalNot>
</XTokenOperEqual>
```
Evaluate to: `True`
***

Input expression: `to_be || !to_be`

Tidy-up format: `to_be || !to_be`

Serialized tree:
```
<XTokenOperLogicalOr>
  <XTokenRefId data="to_be" />
  <XTokenOperLogicalNot>
    <XTokenRefId data="to_be" />
  </XTokenOperLogicalNot>
</XTokenOperLogicalOr>
```
Evaluate to: `True`
***

Input expression: ` maccheroni || spaghetti || rigatoni`

Tidy-up format: `maccheroni || (spaghetti || rigatoni)`

Serialized tree:
```
<XTokenOperLogicalOr>
  <XTokenRefId data="maccheroni" />
  <XTokenOperLogicalOr>
    <XTokenRefId data="spaghetti" />
    <XTokenRefId data="rigatoni" />
  </XTokenOperLogicalOr>
</XTokenOperLogicalOr>
```
Evaluate to: `True`
***

Input expression: ` sex && drug && rock && roll   `

Tidy-up format: `sex && (drug && (rock && roll))`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenRefId data="sex" />
  <XTokenOperLogicalAnd>
    <XTokenRefId data="drug" />
    <XTokenOperLogicalAnd>
      <XTokenRefId data="rock" />
      <XTokenRefId data="roll" />
    </XTokenOperLogicalAnd>
  </XTokenOperLogicalAnd>
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `!me || you && !they `

Tidy-up format: `!me || (you && !they)`

Serialized tree:
```
<XTokenOperLogicalOr>
  <XTokenOperLogicalNot>
    <XTokenRefId data="me" />
  </XTokenOperLogicalNot>
  <XTokenOperLogicalAnd>
    <XTokenRefId data="you" />
    <XTokenOperLogicalNot>
      <XTokenRefId data="they" />
    </XTokenOperLogicalNot>
  </XTokenOperLogicalAnd>
</XTokenOperLogicalOr>
```
Evaluate to: `True`
***

Input expression: `a==b && c!=d`

Tidy-up format: `(a == b) && (c != d)`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenOperEqual>
    <XTokenRefId data="a" />
    <XTokenRefId data="b" />
  </XTokenOperEqual>
  <XTokenOperNotEqual>
    <XTokenRefId data="c" />
    <XTokenRefId data="d" />
  </XTokenOperNotEqual>
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `pname match/abc/`

Tidy-up format: `pname match /abc/`

Serialized tree:
```
<XTokenOperMatch>
  <XTokenRefId data="pname" />
  <XTokenMatchParam data="abc" flags="" />
</XTokenOperMatch>
```
Evaluate to: `True`
***

Input expression: `pname match /xyz/ig`

Test error: **Unsupported Regex match flag: g**
***

Input expression: `pname   match /(\w+)\s(\w+)/`

Tidy-up format: `pname match /(\w+)\s(\w+)/`

Serialized tree:
```
<XTokenOperMatch>
  <XTokenRefId data="pname" />
  <XTokenMatchParam data="(\w+)\s(\w+)" flags="" />
</XTokenOperMatch>
```
Evaluate to: `True`
***

Input expression: `(!me ||you)&&they`

Tidy-up format: `(!me || you) && they`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenOperLogicalOr>
    <XTokenOperLogicalNot>
      <XTokenRefId data="me" />
    </XTokenOperLogicalNot>
    <XTokenRefId data="you" />
  </XTokenOperLogicalOr>
  <XTokenRefId data="they" />
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `!(a=='q') && (b!='x')`

Tidy-up format: `!(a == "q") && (b != "x")`

Serialized tree:
```
<XTokenOperLogicalAnd>
  <XTokenOperLogicalNot>
    <XTokenOperEqual>
      <XTokenRefId data="a" />
      <XTokenString data="q" />
    </XTokenOperEqual>
  </XTokenOperLogicalNot>
  <XTokenOperNotEqual>
    <XTokenRefId data="b" />
    <XTokenString data="x" />
  </XTokenOperNotEqual>
</XTokenOperLogicalAnd>
```
Evaluate to: `True`
***

Input expression: `(a || b) && (c || d) || (e && f)`

Tidy-up format: `((a || b) && (c || d)) || (e && f)`

Serialized tree:
```
<XTokenOperLogicalOr>
  <XTokenOperLogicalAnd>
    <XTokenOperLogicalOr>
      <XTokenRefId data="a" />
      <XTokenRefId data="b" />
    </XTokenOperLogicalOr>
    <XTokenOperLogicalOr>
      <XTokenRefId data="c" />
      <XTokenRefId data="d" />
    </XTokenOperLogicalOr>
  </XTokenOperLogicalAnd>
  <XTokenOperLogicalAnd>
    <XTokenRefId data="e" />
    <XTokenRefId data="f" />
  </XTokenOperLogicalAnd>
</XTokenOperLogicalOr>
```
Evaluate to: `True`
***

Input expression: `! (a && (b && c || d && e) || (g == h && j))`

Tidy-up format: `!((a && ((b && c) || (d && e))) || ((g == h) && j))`

Serialized tree:
```
<XTokenOperLogicalNot>
  <XTokenOperLogicalOr>
    <XTokenOperLogicalAnd>
      <XTokenRefId data="a" />
      <XTokenOperLogicalOr>
        <XTokenOperLogicalAnd>
          <XTokenRefId data="b" />
          <XTokenRefId data="c" />
        </XTokenOperLogicalAnd>
        <XTokenOperLogicalAnd>
          <XTokenRefId data="d" />
          <XTokenRefId data="e" />
        </XTokenOperLogicalAnd>
      </XTokenOperLogicalOr>
    </XTokenOperLogicalAnd>
    <XTokenOperLogicalAnd>
      <XTokenOperEqual>
        <XTokenRefId data="g" />
        <XTokenRefId data="h" />
      </XTokenOperEqual>
      <XTokenRefId data="j" />
    </XTokenOperLogicalAnd>
  </XTokenOperLogicalOr>
</XTokenOperLogicalNot>
```
Evaluate to: `False`
***

Input expression: `!! (((a)==b) && ((((c && ((g)))))))`

Tidy-up format: `!!((a == b) && (c && g))`

Serialized tree:
```
<XTokenOperLogicalNot>
  <XTokenOperLogicalNot>
    <XTokenOperLogicalAnd>
      <XTokenOperEqual>
        <XTokenRefId data="a" />
        <XTokenRefId data="b" />
      </XTokenOperEqual>
      <XTokenOperLogicalAnd>
        <XTokenRefId data="c" />
        <XTokenRefId data="g" />
      </XTokenOperLogicalAnd>
    </XTokenOperLogicalAnd>
  </XTokenOperLogicalNot>
</XTokenOperLogicalNot>
```
Evaluate to: `False`
***

