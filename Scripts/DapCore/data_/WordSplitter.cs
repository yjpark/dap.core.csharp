using System;
using System.Text;
using System.Collections.Generic;

namespace angeldnd.dap {
    public static class WordSplitterConsts {
        public const char EscapeChar = '\\';

        public const char EncloseBeginChar = '`';
        public const char EncloseEndChar = '`';

        public readonly static char[] LineSeparators = new char[]{'\n', '\r'};
        public readonly static char[] WordSeparators = new char[]{' ', '\t'};
        public readonly static char[] EmptyChars = new char[]{' ', '\t', '\n', '\r'};

        public static bool IsWordSeparator(char ch) {
            foreach (char separator in WordSeparators) {
                if (separator == ch) return true;
            }
            return false;
        }

        public static bool IsEmptyChar(char ch) {
            foreach (char empty in EmptyChars) {
                if (empty == ch) return true;
            }
            return false;
        }
    }

    public static class WordSplitter {
        public static void Split(string source, string content, char[] wordChars, Action<Word> processor) {
            if (content == null) return;

            StringBuilder current = new StringBuilder(1024);
            bool currentIsEmpty = true;
            int currentLine = 0;
            int currentColumn = 0;

            Action<int, int> AddCurrent = (int lineZeroBase, int columnZeroBase) => {
                string word = current.ToString().Trim();
                if (!string.IsNullOrEmpty(word)) {
                    processor(new Word(source, currentLine, currentColumn, word));
                }
                currentIsEmpty = true;
                currentLine = lineZeroBase + 1;
                currentColumn = columnZeroBase + 1;
                current.Length = 0;
            };

            Action<int, int, char> AppendToCurrent = (int lineZeroBase, int columnZeroBase, char ch) => {
                current.Append(ch);
                if (currentIsEmpty) {
                    if (!WordSplitterConsts.IsEmptyChar(ch)) {
                        currentIsEmpty = false;
                        currentLine = lineZeroBase + 1;
                        currentColumn = columnZeroBase + 1;
                    }
                }
            };

            bool escaped = false;
            bool enclosed = false;

            string[] lines = content.Split(WordSplitterConsts.LineSeparators);
            for (int i = 0; i < lines.Length; i++) {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++) {
                    char ch = line[j];

                    if (escaped) {
                        AppendToCurrent(i, j, ch);
                        escaped = false;
                    } else {
                        if (ch == WordSplitterConsts.EscapeChar) {
                            escaped = true;
                        } else if (enclosed) {
                            if (ch == WordSplitterConsts.EncloseEndChar) {
                                AddCurrent(i, j);
                                enclosed = false;
                            } else {
                                AppendToCurrent(i, j, ch);
                            }
                        } else if (ch == WordSplitterConsts.EncloseBeginChar) {
                            AddCurrent(i, j);
                            enclosed = true;
                        } else if (WordSplitterConsts.IsWordSeparator(ch)) {
                            AddCurrent(i, j);
                        } else if (wordChars != null && IsCharWord(wordChars, ch)) {
                            AddCurrent(i, j);
                            AppendToCurrent(i, j, ch);
                            AddCurrent(i, j);
                        } else {
                            AppendToCurrent(i, j, ch);
                        }
                    }
                }
                if (!enclosed) {
                    AddCurrent(i, -1);
                }
            }
            AddCurrent(-1, -1);
        }

        private static bool IsCharWord(char[] wordChars, char ch) {
            for (int i = 0; i < wordChars.Length; i++) {
                if (wordChars[i] == ch) {
                    return true;
                }
            }
            return false;
        }

        public static List<Word> Split(string source, string content, char[] wordChars) {
            var result = new List<Word>();
            Split(source, content, wordChars, (Word word) => {
                result.Add(word);
            });
            return result;
        }

        public static void Split(string source, string content, Action<Word> processor) {
            Split(source, content, null, processor);
        }
    }
}