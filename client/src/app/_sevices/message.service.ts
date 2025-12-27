import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CreateMessage } from '../_model/create-message';
import { Message } from '../_model/Message';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  private http = inject(HttpClient);
  baseUrl ="https://localhost:5001/api/";
  messages = signal<Message[]>([]);

  sendMessage(model: CreateMessage) {
    return this.http.post(`${this.baseUrl}messages`, model);
  }

  getMessages(predicate: string) {
    return this.http.get<Message[]>(
      `${this.baseUrl}messages?predicate=${predicate}`
    );
  }

  deleteMessage(messageId: number) {
    return this.http.delete(
      `${this.baseUrl}messages/${messageId}`
    );
  }
}
