import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{   // basic class layout:- 1. properties 
                                                  //  2.constructors 
                                                  //    3.methods


  title = 'Humble';
  users: any; // turning off type safety

  //Now we need to inject httpclient in this ts file (not the module directly, but something we need from the module)
  constructor(private http: HttpClient) {} // pehle obv. component initialize hoga with the help of this constructor then ngOninit()

  ngOnInit(): void {


    //we'll make a request to our API server
    // observable - a stream of data, we need to observe them, they lazy as shit so SUBSCRIBE(subscribe method) to it

    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,  // what we'll do with the data when we get it
      error: error => console.log(error), // if we get an error in return
      complete: () => console.log("Request is completed")
    });

  }



  
}
