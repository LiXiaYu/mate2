// Define a grammar called Hello
grammar MyGrammar;
 
/*
 * Parser Rules
 */
 
program
    : expression
;
 
expression
    : '(' expression ')'    #Parenthesis
    | expression operate = ('*' | '/') expression   #MultiplyDivide
    | expression operate = ('+' | '-') expression   #AddSubtraction    
    | INT   #Number
;
 
 
/*
 * Lexer Rules
 */
 
ADD : '+' ;
SUB : '-' ;
MUL : '*' ;
DIV : '/' ;
 
INT : '0'..'9'+ ;
 
WS : [ \t\r\n]+ -> skip ;