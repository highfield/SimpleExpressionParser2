## Sample output

Expression: `3+5`

Serialized tree: `3 + 5`
```
<XTokenOperAdd>
  <XTokenNumber data="3" />
  <XTokenNumber data="5" />
</XTokenOperAdd>
```
Result: `8`
***

Expression: `3+5 +10`

Serialized tree: `3 + (5 + 10)`
```
<XTokenOperAdd>
  <XTokenNumber data="3" />
  <XTokenOperAdd>
    <XTokenNumber data="5" />
    <XTokenNumber data="10" />
  </XTokenOperAdd>
</XTokenOperAdd>
```
Result: `18`
***

Expression: `3+(5+1) + (2+3) +4`

Serialized tree: `3 + ((5 + 1) + ((2 + 3) + 4))`
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
Result: `18`
***

Expression: ` +7`

Serialized tree: `+7`
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="7" />
</XTokenOperUnaryPlus>
```
Result: `7`
***

Expression: ` + (4)`

Serialized tree: `+4`
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="4" />
</XTokenOperUnaryPlus>
```
Result: `4`
***

Expression: ` (+ +8)`

Serialized tree: `++8`
```
<XTokenOperUnaryPlus>
  <XTokenOperUnaryPlus>
    <XTokenNumber data="8" />
  </XTokenOperUnaryPlus>
</XTokenOperUnaryPlus>
```
Result: `8`
***

Expression: `(+9) == + +9`

Serialized tree: `+9 == ++9`
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
Result: `True`
***

Expression: `3==+3`

Serialized tree: `3 == +3`
```
<XTokenOperEqual>
  <XTokenNumber data="3" />
  <XTokenOperUnaryPlus>
    <XTokenNumber data="3" />
  </XTokenOperUnaryPlus>
</XTokenOperEqual>
```
Result: `True`
***

Expression: `3==--3`

Serialized tree: `3 == --3`
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
Result: `True`
***

Expression: `-(1-5)+(2+7)-10`

Serialized tree: `-(1 - 5) + ((2 + 7) - 10)`
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
Result: `3`
***

Expression: `-10*2+(-2)*5/2`

Serialized tree: `(-10 * 2) + (-2 * (5 / 2))`
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
Result: `-25`
***

Expression: `1+0 / 0`

Serialized tree: `1 + (0 / 0)`
```
<XTokenOperAdd>
  <XTokenNumber data="1" />
  <XTokenOperDiv>
    <XTokenNumber data="0" />
    <XTokenNumber data="0" />
  </XTokenOperDiv>
</XTokenOperAdd>
```
Result: `NaN`
***

Expression: `1+13 % 10`

Serialized tree: `1 + (13 % 10)`
```
<XTokenOperAdd>
  <XTokenNumber data="1" />
  <XTokenOperMod>
    <XTokenNumber data="13" />
    <XTokenNumber data="10" />
  </XTokenOperMod>
</XTokenOperAdd>
```
Result: `4`
***

Expression: `2+3*5`

Serialized tree: `2 + (3 * 5)`
```
<XTokenOperAdd>
  <XTokenNumber data="2" />
  <XTokenOperMul>
    <XTokenNumber data="3" />
    <XTokenNumber data="5" />
  </XTokenOperMul>
</XTokenOperAdd>
```
Result: `17`
***

Expression: `2*3-5`

Serialized tree: `(2 * 3) - 5`
```
<XTokenOperSub>
  <XTokenOperMul>
    <XTokenNumber data="2" />
    <XTokenNumber data="3" />
  </XTokenOperMul>
  <XTokenNumber data="5" />
</XTokenOperSub>
```
Result: `1`
***

Expression: `(10 & 2) == 2`

Serialized tree: `(10 & 2) == 2`
```
<XTokenOperEqual>
  <XTokenOperBitwiseAnd>
    <XTokenNumber data="10" />
    <XTokenNumber data="2" />
  </XTokenOperBitwiseAnd>
  <XTokenNumber data="2" />
</XTokenOperEqual>
```
Result: `True`
***

Expression: `12345 & 56`

Serialized tree: `12345 & 56`
```
<XTokenOperBitwiseAnd>
  <XTokenNumber data="12345" />
  <XTokenNumber data="56" />
</XTokenOperBitwiseAnd>
```
Result: `56`
***

Expression: `(23 | 11) ^ ~66`

Serialized tree: `(23 | 11) ^ ~66`
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
Result: `-94`
***

Expression: ``

Test error: Empty expression.
***

Expression: `    `

Test error: Empty expression.
***

Expression: `false  `

Serialized tree: `false`
```
<XTokenBoolean data="false" />
```
Result: `False`
***

Expression: `  true`

Serialized tree: `true`
```
<XTokenBoolean data="true" />
```
Result: `True`
***

Expression: `  null  `

Serialized tree: `null`
```
<XTokenNull />
```
Result: ``
***

Expression: ` 123 `

Serialized tree: `123`
```
<XTokenNumber data="123" />
```
Result: `123`
***

Expression: `-456  `

Serialized tree: `-456`
```
<XTokenOperUnaryMinus>
  <XTokenNumber data="456" />
</XTokenOperUnaryMinus>
```
Result: `-456`
***

Expression: ` +7 `

Serialized tree: `+7`
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="7" />
</XTokenOperUnaryPlus>
```
Result: `7`
***

Expression: ` 3.151927 `

Serialized tree: `3.151927`
```
<XTokenNumber data="3.151927" />
```
Result: `3,151927`
***

Expression: ` +2.718 `

Serialized tree: `+2.718`
```
<XTokenOperUnaryPlus>
  <XTokenNumber data="2.718" />
</XTokenOperUnaryPlus>
```
Result: `2,718`
***

Expression: ` .5 `

Test error: Illegal character found: .
***

Expression: ` -.5 `

Test error: Illegal character found: .
***

Expression: ` +5. `

Test error: Illegal character found:  
***

Expression: `myvar  `

Serialized tree: `myvar`
```
<XTokenRefId data="myvar" />
```
Result: `MyVar`
***

Expression: `  w0rd`

Serialized tree: `w0rd`
```
<XTokenRefId data="w0rd" />
```
Result: ``
***

Expression: `_abc_def_`

Serialized tree: `_abc_def_`
```
<XTokenRefId data="_abc_def_" />
```
Result: `3,141592653589793`
***

Expression: `my.multi.level.reference`

Serialized tree: `my.multi.level.reference`
```
<XTokenRefId data="my.multi.level.reference" />
```
Result: ``
***

Expression: ` 'incomplete string`

Test error: Unexpected end of string.
***

Expression: ` '`

Test error: Unexpected end of string.
***

Expression: ` ''`

Serialized tree: `""`
```
<XTokenString data="" />
```
Result: ``
***

Expression: `'' `

Serialized tree: `""`
```
<XTokenString data="" />
```
Result: ``
***

Expression: `'single-quoted string'`

Serialized tree: `"single-quoted string"`
```
<XTokenString data="single-quoted string" />
```
Result: `single-quoted string`
***

Expression: `"double-quoted string"`

Serialized tree: `"double-quoted string"`
```
<XTokenString data="double-quoted string" />
```
Result: `double-quoted string`
***

Expression: `'here is a "nested string"'`

Serialized tree: `"here is a "nested string""`
```
<XTokenString data="here is a &quot;nested string&quot;" />
```
Result: `here is a "nested string"`
Verify error: Missing operator.
***

Expression: `'here is a "nested escaped string"'`

Serialized tree: `"here is a "nested escaped string""`
```
<XTokenString data="here is a &quot;nested escaped string&quot;" />
```
Result: `here is a "nested escaped string"`
Verify error: Missing operator.
***

Expression: ` #  `

Test error: Unexpected end of ISO8601 sequence.
***

Expression: ` #`

Test error: Unexpected end of ISO8601 sequence.
***

Expression: ` ##  `

Test error: Unsupported ISO8601 format: 
***

Expression: ` # #`

Test error: Unsupported ISO8601 format:  
***

Expression: ` #à #  `

Test error: Unsupported ISO8601 format: à 
***

Expression: ` #2007-04-05ù12:30-02:00#  `

Test error: The string '2007-04-05ù12:30-02:00' was not recognized as a valid DateTime. There is an unknown word starting at index '10'.
***

Expression: ` #2007-03-01T13:00:00Z# `

Serialized tree: `#2007-03-01T13:00:00.0000000Z#`
```
<XTokenDateTime data="2007-03-01T13:00:00Z" />
```
Result: `01/03/2007 13:00:00`
***

Expression: ` #2007-04-05T12:30-02:00#  `

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00#`
```
<XTokenDateTime data="2007-04-05T16:30:00+02:00" />
```
Result: `05/04/2007 16:30:00`
***

Expression: ` #2007-04-05T12:30-02:00#== #2007-04-05T12:30-02:00# `

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00# == #2007-04-05T16:30:00.0000000+02:00#`
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
</XTokenOperEqual>
```
Result: `True`
***

Expression: ` #2007-04-05T12:30-02:00# < #2007-04-05T12:31-02:00#`

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00# < #2007-04-05T16:31:00.0000000+02:00#`
```
<XTokenOperLessThan>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T16:31:00+02:00" />
</XTokenOperLessThan>
```
Result: `True`
***

Expression: ` #2007-04-05T12:30-02:00# != 123 `

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00# != 123`
```
<XTokenOperNotEqual>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenNumber data="123" />
</XTokenOperNotEqual>
```
Test error: Cannot convert a DateTime to a Double.
***

Expression: ` #2007-04-05T12:30-02:00# + 123 `

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00# + 123`
```
<XTokenOperAdd>
  <XTokenDateTime data="2007-04-05T16:30:00+02:00" />
  <XTokenNumber data="123" />
</XTokenOperAdd>
```
Test error: Cannot convert a DateTime to a Double.
***

Expression: ` #2007-04-05T12:30-02:00# #2007-04-05T12:30-02:00# `

Test error: Missing operator.
***

Expression: ` #2007-04-05T12:30-02:00##2007-04-05T12:30-02:00# `

Test error: Missing operator.
***

Expression: ` (#2007-04-05T12:30-02:00#)`

Serialized tree: `#2007-04-05T16:30:00.0000000+02:00#`
```
<XTokenDateTime data="2007-04-05T16:30:00+02:00" />
```
Result: `05/04/2007 16:30:00`
***

Expression: ` #2007-04-05T12:30+02:00# == #2007-04-05T08:30:00-02:00#`

Serialized tree: `#2007-04-05T12:30:00.0000000+02:00# == #2007-04-05T12:30:00.0000000+02:00#`
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
</XTokenOperEqual>
```
Result: `True`
***

Expression: ` #2007-04-05T12:30+02:00# == #2007-04-05T10:30:00Z#`

Serialized tree: `#2007-04-05T12:30:00.0000000+02:00# == #2007-04-05T10:30:00.0000000Z#`
```
<XTokenOperEqual>
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
  <XTokenDateTime data="2007-04-05T10:30:00Z" />
</XTokenOperEqual>
```
Result: `False`
***

Expression: ` #3000-04-05T12:30+02:00#`

Serialized tree: `#3000-04-05T12:30:00.0000000+02:00#`
```
<XTokenDateTime data="3000-04-05T12:30:00+02:00" />
```
Result: `05/04/3000 12:30:00`
***

Expression: ` #2018-04-05T12:30:45.123456789+02:00#`

Serialized tree: `#2018-04-05T12:30:45.1234568+02:00#`
```
<XTokenDateTime data="2018-04-05T12:30:45.1234568+02:00" />
```
Result: `05/04/2018 12:30:45`
***

Expression: ` #2007-04-05T12:30:45.123+02:00# > #2007-04-05T08:30:00-02:00#`

Serialized tree: `#2007-04-05T12:30:45.1230000+02:00# > #2007-04-05T12:30:00.0000000+02:00#`
```
<XTokenOperGreaterThan>
  <XTokenDateTime data="2007-04-05T12:30:45.123+02:00" />
  <XTokenDateTime data="2007-04-05T12:30:00+02:00" />
</XTokenOperGreaterThan>
```
Result: `True`
***

Expression: `zero == zero  `

Serialized tree: `zero == zero`
```
<XTokenOperEqual>
  <XTokenRefId data="zero" />
  <XTokenRefId data="zero" />
</XTokenOperEqual>
```
Result: `True`
***

Expression: ` black != white`

Serialized tree: `black != white`
```
<XTokenOperNotEqual>
  <XTokenRefId data="black" />
  <XTokenRefId data="white" />
</XTokenOperNotEqual>
```
Result: `True`
***

Expression: ` 12 < 45`

Serialized tree: `12 < 45`
```
<XTokenOperLessThan>
  <XTokenNumber data="12" />
  <XTokenNumber data="45" />
</XTokenOperLessThan>
```
Result: `True`
***

Expression: `20 >4`

Serialized tree: `20 > 4`
```
<XTokenOperGreaterThan>
  <XTokenNumber data="20" />
  <XTokenNumber data="4" />
</XTokenOperGreaterThan>
```
Result: `True`
***

Expression: `10<=100`

Serialized tree: `10 <= 100`
```
<XTokenOperLessOrEqualThan>
  <XTokenNumber data="10" />
  <XTokenNumber data="100" />
</XTokenOperLessOrEqualThan>
```
Result: `True`
***

Expression: `100   >=   1`

Serialized tree: `100 >= 1`
```
<XTokenOperGreaterOrEqualThan>
  <XTokenNumber data="100" />
  <XTokenNumber data="1" />
</XTokenOperGreaterOrEqualThan>
```
Result: `True`
***

Expression: `true && (1 < 2)`

Serialized tree: `true && (1 < 2)`
```
<XTokenOperLogicalAnd>
  <XTokenBoolean data="true" />
  <XTokenOperLessThan>
    <XTokenNumber data="1" />
    <XTokenNumber data="2" />
  </XTokenOperLessThan>
</XTokenOperLogicalAnd>
```
Result: `True`
***

Expression: `(3 < 5) && true`

Serialized tree: `(3 < 5) && true`
```
<XTokenOperLogicalAnd>
  <XTokenOperLessThan>
    <XTokenNumber data="3" />
    <XTokenNumber data="5" />
  </XTokenOperLessThan>
  <XTokenBoolean data="true" />
</XTokenOperLogicalAnd>
```
Result: `True`
***

Expression: `true && true`

Serialized tree: `true && true`
```
<XTokenOperLogicalAnd>
  <XTokenBoolean data="true" />
  <XTokenBoolean data="true" />
</XTokenOperLogicalAnd>
```
Result: `True`
***

Expression: `false || (true) || (1 == 2)`

Serialized tree: `false || (true || (1 == 2))`
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
Result: `True`
***

Expression: `(1==1) && (2==2) && true`

Serialized tree: `(1 == 1) && ((2 == 2) && true)`
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
Result: `True`
***

Expression: `!false==!!true`

Serialized tree: `!false == !!true`
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
Result: `True`
***

Expression: `to_be || !to_be`

Serialized tree: `to_be || !to_be`
```
<XTokenOperLogicalOr>
  <XTokenRefId data="to_be" />
  <XTokenOperLogicalNot>
    <XTokenRefId data="to_be" />
  </XTokenOperLogicalNot>
</XTokenOperLogicalOr>
```
Result: `True`
***

Expression: ` maccheroni || spaghetti || rigatoni`

Serialized tree: `maccheroni || (spaghetti || rigatoni)`
```
<XTokenOperLogicalOr>
  <XTokenRefId data="maccheroni" />
  <XTokenOperLogicalOr>
    <XTokenRefId data="spaghetti" />
    <XTokenRefId data="rigatoni" />
  </XTokenOperLogicalOr>
</XTokenOperLogicalOr>
```
Result: `True`
***

Expression: ` sex && drug && rock && roll   `

Serialized tree: `sex && (drug && (rock && roll))`
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
Result: `True`
***

Expression: `!me || you && !they `

Serialized tree: `!me || (you && !they)`
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
Result: `True`
***

Expression: `a==b && c!=d`

Serialized tree: `(a == b) && (c != d)`
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
Result: `True`
***

Expression: `pname match/abc/`

Serialized tree: `pname match /abc/`
```
<XTokenOperMatch>
  <XTokenRefId data="pname" />
  <XTokenMatchParam data="abc" flags="" />
</XTokenOperMatch>
```
Result: `True`
***

Expression: `pname match /xyz/ig`

Test error: Unsupported Regex match flag: g
***

Expression: `pname   match /(\w+)\s(\w+)/`

Serialized tree: `pname match /(\w+)\s(\w+)/`
```
<XTokenOperMatch>
  <XTokenRefId data="pname" />
  <XTokenMatchParam data="(\w+)\s(\w+)" flags="" />
</XTokenOperMatch>
```
Result: `True`
***

Expression: `(!me ||you)&&they`

Serialized tree: `(!me || you) && they`
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
Result: `True`
***

Expression: `!(a=='q') && (b!='x')`

Serialized tree: `!(a == "q") && (b != "x")`
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
Result: `True`
***

Expression: `(a || b) && (c || d) || (e && f)`

Serialized tree: `((a || b) && (c || d)) || (e && f)`
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
Result: `True`
***

Expression: `! (a && (b && c || d && e) || (g == h && j))`

Serialized tree: `!((a && ((b && c) || (d && e))) || ((g == h) && j))`
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
Result: `False`
***

Expression: `!! (((a)==b) && ((((c && ((g)))))))`

Serialized tree: `!!((a == b) && (c && g))`
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
Result: `False`
***

