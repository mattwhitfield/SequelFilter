namespace SequelFilter
{
    using System;
    using Irony.Parsing;

    public class FieldListGrammar : Grammar
    {
        // terminals
        public const string FieldReference = "FieldReference";
        public const string FieldReferenceList = "FieldReferenceList";

        // internal identifiers
        private const string Dot = ".";
        private const string Comma = ",";

        public FieldListGrammar()
            : base(false)
        {
            // literals
            var fieldReferenceElement = TerminalFactory.CreateSqlExtIdentifier(this, "fieldReferenceElement");

            // lists
            var fieldReference = new NonTerminal(FieldReference);
            MakePlusRule(fieldReference, ToTerm(Dot), fieldReferenceElement);
            var fieldReferenceList = new NonTerminal(FieldReferenceList);
            MakePlusRule(fieldReferenceList, ToTerm(Comma), fieldReference);

            Root = fieldReferenceList;
            MarkPunctuation(Dot, Comma);
        }
    }
}
