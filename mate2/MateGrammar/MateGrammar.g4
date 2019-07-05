// Define a grammar called Mate
grammar MateGrammar;

/*
 * Parser Rules
 */

program
    : expression
;
 
expression
    : mate_define #ExpressionMateDefine
    | var_define #ExpressionVarDefine
   //  | none_expression #ExpressionNone
   //  | Schar* #ExpressionSchars
;
none_expression
    :
;
block
	: '{' expression '}'
;

mate_define
	: MATE T标识符 block ';'   #MateDefineWithBlock
	| MATE T标识符 ';'   #MateDefineWithoutBlock
;

string
    :Stringliteral
;
varval
   : string
   |
   ;
var_define
    :T类型名 T标识符 ';' #VarDefineWithoutVarVal
    |T类型名 T标识符 '=' varval ';' #VarDefineWithVarval
;
/*
 * Lexer Rules
 */

MATE : 'mate';

WS : [ \t\r\n]+ -> skip ;

T标识符
	: ('a' .. 'z' | 'A' .. 'Z' | '\u4E00'..'\u9FA5' | '\uF900'..'\uFA2D')+ 
;
T类型名
	: ('a' .. 'z' | 'A' .. 'Z' | '\u4E00'..'\u9FA5' | '\uF900'..'\uFA2D')+ 
;
Stringliteral
   : Encodingprefix? '"' Schar* '"'
   | Encodingprefix? 'R' Rawstring
   ;

fragment Encodingprefix
   : 'u8'
   | 'u'
   | 'U'
   | 'L'
   ;

fragment Schar
   : ~ ["\\\r\n]
   | Escapesequence
   | Universalcharactername
   ;
fragment Rawstring
   : '"' .*? '(' .*? ')' .*? '"'
   ;

fragment Escapesequence
   : Simpleescapesequence
   | Octalescapesequence
   | Hexadecimalescapesequence
   ;

fragment Simpleescapesequence
   : '\\\''
   | '\\"'
   | '\\?'
   | '\\\\'
   | '\\a'
   | '\\b'
   | '\\f'
   | '\\n'
   | '\\r'
   | '\\t'
   | '\\v'
   ;

fragment Octalescapesequence
   : '\\' OCTALDIGIT
   | '\\' OCTALDIGIT OCTALDIGIT
   | '\\' OCTALDIGIT OCTALDIGIT OCTALDIGIT
   ;

fragment Hexadecimalescapesequence
   : '\\x' HEXADECIMALDIGIT+
   ;

fragment Universalcharactername
   : '\\u' Hexquad
   | '\\U' Hexquad Hexquad
   ;

fragment OCTALDIGIT
   : [0-7]
   ;
fragment HEXADECIMALDIGIT
   : [0-9a-fA-F]
   ;
fragment Hexquad
   : HEXADECIMALDIGIT HEXADECIMALDIGIT HEXADECIMALDIGIT HEXADECIMALDIGIT
   ;