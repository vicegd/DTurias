import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Repository } from "../app/models/repository";
import { Tweet } from "../app/models/tweet.model";

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    constructor(private repo: Repository, http: Http, @Inject('BASE_URL') baseUrl: string) {
        console.log(baseUrl);
    }

    get tweets(): Tweet[] {
        return this.repo.tweets;
    }

    deleteTweet(id: number) {
        this.repo.deleteTweet(id);
    }

    updateTweet(id: number, sentiment : number) {
        this.repo.updateTweet(id, sentiment);
    }
}