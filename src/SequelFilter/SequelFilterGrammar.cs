namespace SequelFilter
{
    using System;
    using Irony.Parsing;

    public class SequelFilterGrammar : Grammar
    {
        // terminals
        public const string NumberLiteral = "NumberLiteral";
        public const string StringLiteral = "StringLiteral";
        public const string FieldReference = "FieldReference";
        public const string LiteralList = "LiteralList";

        // expressions
        public const string BetweenExpression = "BetweenExpression";
        public const string BinaryExpression = "BinaryExpression";
        public const string ComparisonExpression = "ComparisonExpression";
        public const string EnumerableExpression = "EnumerableExpression";
        public const string InExpression = "InExpression";
        public const string IsNullExpression = "IsNullExpression";
        public const string LikeExpression = "LikeExpression";
        public const string NotExpression = "NotExpression";

        // operators
        public const string And = "AND";
        public const string And_Symbol = "&&";
        public const string Between = "BETWEEN";
        public const string Equal_Symbol = "=";
        public const string Equals_Symbol = "==";
        public const string GreaterThan_Symbol = ">";
        public const string GreaterThanEqualTo_Symbol = ">=";
        public const string HasAny = "HAS_ANY";
        public const string HasNone = "HAS_NONE";
        public const string HasSingle = "HAS_SINGLE";
        public const string In = "IN";
        public const string Is = "IS";
        public const string LessThan_Symbol = "<";
        public const string LessThanEqualTo_Symbol = "<=";
        public const string Like = "LIKE";
        public const string Or = "OR";
        public const string Or_Symbol = "||";
        public const string Not = "NOT";
        public const string Not_Equal_Symbol = "<>";
        public const string Not_Equals_Symbol = "!=";
        public const string Null = "NULL";

        // internal identifiers
        private const string Dot = ".";
        private const string Comma = ",";
        private const string Comment = "Comment";
        private const string OpenParen = "(";
        private const string CloseParen = ")";
        private const string GoesInto = "=>";
        private const string LineFeed = "\n";
        private const string CarriageReturnLineFeed = "\r\n";
        private const string MultiLineCommentStart = "/*";
        private const string MultiLineCommentEnd = "*/";
        private const string CStyleCommentStart = "//";
        private const string SQLStyleCommentStart = "--";
        private const string SingleQuote = "'";

        public SequelFilterGrammar()
            : base(false)
        {
            // comments
            NonGrammarTerminals.Add(new CommentTerminal(Comment, MultiLineCommentStart, MultiLineCommentEnd));
            NonGrammarTerminals.Add(new CommentTerminal(Comment, CStyleCommentStart, LineFeed, CarriageReturnLineFeed));
            NonGrammarTerminals.Add(new CommentTerminal(Comment, SQLStyleCommentStart, LineFeed, CarriageReturnLineFeed));

            // literals
            var numberLiteral = new NumberLiteral(NumberLiteral) { DefaultIntTypes = new[] { TypeCode.Int64 }, DefaultFloatType = TypeCode.Double };
            var stringLiteral = new StringLiteral(StringLiteral, SingleQuote, StringOptions.AllowsDoubledQuote);
            var fieldReferenceElement = TerminalFactory.CreateSqlExtIdentifier(this, "fieldReferenceElement");

            // non-terminals
            var betweenExpression = new NonTerminal(BetweenExpression);
            var binaryExpression = new NonTerminal(BinaryExpression);
            var comparisonExpression = new NonTerminal(ComparisonExpression);
            var enumerableExpression = new NonTerminal(EnumerableExpression);
            var inExpression = new NonTerminal(InExpression);
            var isNullExpr = new NonTerminal(IsNullExpression);
            var likeExpression = new NonTerminal(LikeExpression);
            var notExpression = new NonTerminal(NotExpression);

            // transient non-terminals
            var binaryOperator = new NonTerminal("binaryOperator");
            var comparisonOperator = new NonTerminal("comparisonOperator");
            var enumerableOperator = new NonTerminal("enumerableOperator");
            var expression = new NonTerminal("expression");
            var literal = new NonTerminal("literal");
            var optionalNot = new NonTerminal("optionalNot");
            var parenthesizedExpression = new NonTerminal("parenthesizedExpression");
            var terminal = new NonTerminal("terminal");
            MarkTransient(binaryOperator, comparisonOperator, enumerableOperator, expression, literal, optionalNot, parenthesizedExpression, terminal);

            // lists
            var fieldReference = new NonTerminal(FieldReference);
            MakePlusRule(fieldReference, ToTerm(Dot), fieldReferenceElement);
            var literalList = new NonTerminal(LiteralList);
            MakePlusRule(literalList, ToTerm(Comma), literal);

            Root = expression;

            // transient non-terminals
            binaryOperator.Rule = ToTerm(And) | Or | And_Symbol | Or_Symbol;
            binaryOperator.SetFlag(TermFlags.InheritPrecedence);
            comparisonOperator.Rule = ToTerm(Equal_Symbol) | Equals_Symbol | GreaterThan_Symbol | LessThan_Symbol | GreaterThanEqualTo_Symbol | LessThanEqualTo_Symbol | Not_Equal_Symbol | Not_Equals_Symbol;
            comparisonOperator.SetFlag(TermFlags.InheritPrecedence);
            enumerableOperator.Rule = ToTerm(HasAny) | HasNone | HasSingle;
            enumerableOperator.SetFlag(TermFlags.InheritPrecedence);
            expression.Rule = binaryExpression | comparisonExpression | inExpression | betweenExpression | likeExpression | isNullExpr | parenthesizedExpression | enumerableExpression | notExpression | fieldReference;
            literal.Rule = stringLiteral | numberLiteral;
            optionalNot.Rule = Empty | Not;
            parenthesizedExpression.Rule = OpenParen + expression + CloseParen;
            terminal.Rule = fieldReference | stringLiteral | numberLiteral;

            // non-terminals
            betweenExpression.Rule = terminal + optionalNot + Between + terminal + And + terminal;
            binaryExpression.Rule = expression + binaryOperator + expression;
            comparisonExpression.Rule = terminal + comparisonOperator + terminal;
            enumerableExpression.Rule = fieldReference + enumerableOperator + fieldReferenceElement + GoesInto + expression;
            inExpression.Rule = terminal + optionalNot + In + OpenParen + literalList + CloseParen;
            isNullExpr.Rule = terminal + Is + optionalNot + Null;
            likeExpression.Rule = terminal + optionalNot + Like + stringLiteral;
            notExpression.Rule = Not + expression;

            RegisterOperators(4, Equal_Symbol, Equals_Symbol, GreaterThan_Symbol, LessThan_Symbol, GreaterThanEqualTo_Symbol, LessThanEqualTo_Symbol, Not_Equal_Symbol, Not_Equals_Symbol, Like, In, Between, Is, Null);
            RegisterOperators(3, Not);
            RegisterOperators(2, And, And_Symbol);
            RegisterOperators(1, Or, Or_Symbol);
            MarkPunctuation(Dot, Comma, OpenParen, CloseParen, GoesInto);
        }
    }
}
