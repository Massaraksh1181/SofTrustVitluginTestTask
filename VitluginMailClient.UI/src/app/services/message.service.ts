import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private messagesApiUrl = 'https://localhost:7210/api/Messages'; 
  private topicsApiUrl = 'https://localhost:7210/api/Topics';  


  constructor(private http: HttpClient) { } 

  getMessageDetails(messageId: number): Observable<any> {
    return this.http.get<any>(`${this.messagesApiUrl}/GetMessageDetails/${messageId}`);
  }

  createMessage(input: any): Observable<any> {
    return this.http.post<any>(`${this.messagesApiUrl}/Create`, input);
  }

  getTopics(): Observable<any[]> {
    return this.http.get<any[]>(`${this.topicsApiUrl}/GetAll`);
  }
}
