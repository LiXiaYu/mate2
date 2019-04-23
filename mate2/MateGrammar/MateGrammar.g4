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
	: MATE ID block ';'   #MateDefineWithBlock
	| MATE ID ';'   #MateDefineWithoutBlock
;
 
/*
 * Lexer Rules
 */

MATE : 'mate';

WS : [ \t\r\n]+ -> skip ;

ID : [a-zA-Z]+ ;