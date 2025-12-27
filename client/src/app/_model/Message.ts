export interface Message {
  id: number;
  senderName: string;
  recipientName: string;
  content: string;
  dateSent: string;
  dateRead: string | null;
}
