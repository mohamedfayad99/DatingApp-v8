import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { MessagesService } from '../_sevices/message.service';
import { Message } from '../_model/Message';
import { Router } from '@angular/router';

@Component({
  selector: 'app-message',
  standalone: true,
  imports: [CommonModule, FormsModule, ButtonsModule],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessageComponent implements OnInit {

  private messageService = inject(MessagesService);
  private router = inject(Router);

  messages: Message[] = [];
  predicate: 'sent' | 'received' = 'received';

  // 🔹 Create message form
  showCreateForm = false;
  recipientName = '';
  content = '';

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.predicate).subscribe({
      next: res => this.messages = res
    });
  }

  switch(predicate: 'sent' | 'received') {
    this.predicate = predicate;
    this.loadMessages();
  }
sendMessage() {
  if (!this.recipientName || !this.content) return;

  this.messageService.sendMessage({
    recipientUsername: this.recipientName,
    content: this.content
  }).subscribe({
    next: () => {
      this.content = '';
      this.recipientName = '';
      this.showCreateForm = false;
      this.switch('sent');
    },
    error: err => {
      if (err.status === 404) {
        alert('User not found');
        this.router.navigate(['/']); // 👈 redirect home
      } else {
        alert('Something went wrong while sending message');
      }
    }
  });
}


  deleteMessage(id: number) {
  this.messageService.deleteMessage(id).subscribe({
    next: () => {
      this.messages = this.messages.filter(m => m.id !== id);
      alert('Message has been deleted'); // ✅ success alert
    },
    error: () => {
      alert('Something went wrong');
    }
  });
}

}
