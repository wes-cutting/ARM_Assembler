using Assembler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Tokenizer
{
    class TokenMaker
    {
        private String NextSymbolSet;
        public List<String> CurrentInputSet { get; set; }
        public Dictionary<int, string> LabelTable { get; set; }

        public TokenMaker(List<String> buffer)
        {
            CurrentInputSet = buffer;
            LabelTable = new Dictionary<int, string>();
        }

        public List<Token> MakeTokens()
        {
            List<Token> tokens = new List<Token>();
            while (CurrentInputSet.Count > 0)
            {
                Token token = new Token();
                String line = CurrentInputSet.ElementAt(0);
 
                String[] labelSplit = line.Split(':');
                if (labelSplit.Count() == 2)
                {
                    LabelTable.Add(tokens.Count, labelSplit[0]);
                    token.Label = labelSplit[0];
                    line = labelSplit[1];
                }

                String[] contentSplit = line.Split(' ');
                token.Operation = ParseOperation(contentSplit[0]);
                token.Condition = ParseCondition(contentSplit[1]);
                token.OpContent = ParseContent(contentSplit[2], token.Operation);

                tokens.Add(token);

                CurrentInputSet.RemoveAt(0);
            }

            return tokens;
        }

        public Operations ParseOperation(string opcode)
        {
            Operations result;
            opcode = opcode.ToUpper();
            if(opcode.Equals("AND"))
            {
                result = Operations.AND;
            }
            else if(opcode.Equals("ORR"))
            {
                result = Operations.ORR;
            }
            else if(opcode.Equals("ADD"))
            {
                result = Operations.ADD;
            }
            else if(opcode.Equals("SUB"))
            {
                result = Operations.SUB;
            }
            else if(opcode.Equals("CMP"))
            {
                result = Operations.CMP;
            }
            else if(opcode.Equals("MOV"))
            {
                result = Operations.MOV;
            }
            else if(opcode.Equals("LDR"))
            {
                result = Operations.LDR;
            }
            else if(opcode.Equals("LDRB"))
            {
                result = Operations.LDRB;
            }
            else if(opcode.Equals("STR"))
            {
                result = Operations.STR;
            }
            else if (opcode.Equals("STRB"))
            {
                result = Operations.STRB;
            }
            else
            {
                result = Operations.BRANCH;
            }
            return result; 
        }
        public Conditions ParseCondition(string condition)
        {
            Conditions result;
            condition = condition.ToUpper();
            if (condition.Equals("EQ"))
            {
                result = Conditions.EQUAL;
            }
            else if (condition.Equals("NE"))
            {
                result = Conditions.NOT_EQUAL;
            }
            else if (condition.Equals("LT"))
            {
                result = Conditions.LESS_THAN;
            }
            else if (condition.Equals("LE"))
            {
                result = Conditions.LESS_OR_EQUAL;
            }
            else if (condition.Equals("GT"))
            {
                result = Conditions.GREATER_THAN;
            }
            else if (condition.Equals("GE"))
            {
                result = Conditions.GREATER_OR_EQUAL;
            }
            else
            {
                result = Conditions.ALWAYS;
            }
            return result;
        }
        public IContent ParseContent(string content, Operations optype)
        {
            IContent result;
            String[] contentParts = content.Split(',');
            if (optype.Equals(Operations.AND) ||
                optype.Equals(Operations.ORR) ||
                optype.Equals(Operations.AND) ||
                optype.Equals(Operations.SUB))
            {
                Params3 opContent = new Params3();
                if (contentParts[0].StartsWith("I"))
                {
                    opContent.IsImmediate = true;
                    contentParts[0] = contentParts[0].Split('I')[1];
                    opContent.Destination = ParseRegister(contentParts[0]);
                    opContent.Operand2 = ParseRegister(contentParts[1]);
                    opContent.Shift = Int32.Parse(contentParts[2]);
                }
                else
                {
                    opContent.Destination = ParseRegister(contentParts[0]);
                    opContent.Operand1 = ParseRegister(contentParts[1]);
                    opContent.Operand2 = ParseRegister(contentParts[2]);
                    opContent.Shift = Int32.Parse(contentParts[3]);
                }
                result = opContent;
            }
            else if (optype.Equals(Operations.CMP) ||
                        optype.Equals(Operations.MOV))
            {
                Params2 opContent = new Params2();
                if (contentParts[0].StartsWith("I"))
                {
                    opContent.IsImmediate = true;
                    contentParts[0] = contentParts[0].Split('I')[1];
                    opContent.Destination = ParseRegister(contentParts[0]);
                    opContent.Shift = Int32.Parse(contentParts[1]);
                }
                else
                {
                    opContent.Destination = ParseRegister(contentParts[0]);
                    opContent.Parameter = ParseRegister(contentParts[1]);
                    opContent.Shift = Int32.Parse(contentParts[2]);
                }
               
                result = opContent;
            }
            else if (optype.Equals(Operations.LDR) ||
                        optype.Equals(Operations.LDRB) ||
                        optype.Equals(Operations.STR) ||
                        optype.Equals(Operations.STRB))
            {
                SignedParam opContent = new SignedParam();
                if (contentParts[0].StartsWith("I"))
                {
                    opContent.IsImmediate = true;
                    contentParts[0] = contentParts[0].Split('I')[1];
                    opContent.ValueReg = ParseRegister(contentParts[0]);
                    opContent.BaseAddress = ParseRegister(contentParts[1]);
                    opContent.Signed = Int32.Parse(contentParts[2]);
                }
                else
                {
                    opContent.ValueReg = ParseRegister(contentParts[0]);
                    opContent.BaseAddress = ParseRegister(contentParts[1]);
                    opContent.Offset = ParseRegister(contentParts[2]); 
                    opContent.Signed = Int32.Parse(contentParts[3]);
                }
                result = opContent;
            }
            else
            {
                BranchName opContent = new BranchName();
                opContent.Name = contentParts[0];
                result = opContent;
            }

            return result;
        }
        private char ParseRegister(string param)
        {
            param = param.Split('R')[1];
            char result;
            switch (param)
            {
                case "1":
                    result = '1';
                    break;
                case "2":
                    result = '2';
                    break;
                case "3":
                    result = '3';
                    break;
                case "4":
                    result = '4';
                    break;
                case "5":
                    result = '5';
                    break;
                case "6":
                    result = '6';
                    break;
                case "7":
                    result = '7';
                    break;
                case "8":
                    result = '8';
                    break;
                case "9":
                    result = '9';
                    break;
                case "10":
                    result = 'A';
                    break;
                case "11":
                    result = 'B';
                    break;
                case "12":
                    result = 'C';
                    break;
                case "13":
                    result = 'D';
                    break;
                case "14":
                    result = 'E';
                    break;
                case "15":
                    result = 'F';
                    break;
                default:
                    result = '0';
                    break;
            }
            return result;
        }
    }

    
}
