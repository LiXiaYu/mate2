// Define a grammar called Hello
grammar MateGrammar;
 
/*
 * Parser Rules
 */
 
program
    : expression
;
 
expression
    : mate_define
;

block
	: '{' expression '}'
;

mate_define
	: MATE T标识符 block ';'   #MateDefineWithBlock
	| MATE T标识符 ';'   #MateDefineWithoutBlock
;

test_id
	: Identifier ';'
;

/*
 * Lexer Rules
 */

MATE : 'mate';

WS : [ \t\r\n]+ -> skip ;



fragment Identifiernondigit
   : NONDIGIT
   | Universalcharactername
   ;

fragment NONDIGIT
   : [a-zA-Z_]
   ;

fragment DIGIT
   : [0-9]
   ;

fragment HEXADECIMALDIGIT
   : [0-9a-fA-F]
   ;

fragment Hexquad
   : HEXADECIMALDIGIT HEXADECIMALDIGIT HEXADECIMALDIGIT HEXADECIMALDIGIT
   ;

fragment Universalcharactername
   : '\\u' Hexquad
   | '\\U' Hexquad Hexquad
   ;

Identifier
   :
/*
   Identifiernondigit
   | Identifier Identifiernondigit
   | Identifier DIGIT
   */
   Identifiernondigit (Identifiernondigit | DIGIT)*
   ;

T标识符
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