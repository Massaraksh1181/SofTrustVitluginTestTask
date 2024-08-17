import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService } from '../services/message.service';

@Component({
  selector: 'app-message-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './message-form.component.html',
  styleUrls: ['./message-form.component.css']
})
export class MessageFormComponent implements OnInit {
  name: string = '';
  email: string = '';
  phone: string = '';
  topicId: number = 0;
  text: string = '';
  topics: any[] = [];
  messageDetails: any = null;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.messageService.getTopics().subscribe(
      data => this.topics = data,
      error => console.error('Error: ', error)
    );
  }

  submitForm(): void {
    const input = {
      Name: this.name,
      Email: this.email,
      Phone: this.phone,
      TopicId: this.topicId,
      Text: this.text
    };

    this.messageService.createMessage(input).subscribe(
      response => {
        this.getMessageDetails(response.id);
      },
      error => console.error('Error: ', error)
    );
  }

  getMessageDetails(messageId: number): void {
    this.messageService.getMessageDetails(messageId).subscribe(
      data => this.messageDetails = data,
      error => console.error('Error: ', error)
    );
  }
}
