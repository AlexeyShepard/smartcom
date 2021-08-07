<template>
    <div class="auth">
        <b-container>
            <b-row>

                <form action="">
                    <div class="modal-card" style="width: 500px" >
                        <header class="modal-card-head">
                            <p class="modal-card-title">Авторизация</p>
                        </header>
                        <section class="modal-card-body">
                            <b-field label="Email">
                                <b-input
                                    type="email"
                                    :value="email"
                                    placeholder="Ваш email"
                                    required
                                    v-model="email">
                                </b-input>
                            </b-field>

                            <b-field label="Password">
                                <b-input
                                    type="password"
                                    :value="password"
                                    password-reveal
                                    placeholder="Ваш пароль"
                                    required
                                    v-model="password">
                                </b-input>
                            </b-field>
                        </section>
                        <footer class="modal-card-foot">
                            <button class="button is-primary" @click="SignIn()">Войти</button>
                        </footer>
                    </div>
                </form>

            </b-row>         
        </b-container>               
    </div>
</template>

<script>

import axios from 'axios'

export default {
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        SignIn() {

            /*axios.post('http://localhost:11549/Account', {

                    Name: "Рандом",
                    Email: this.email,
                    Password: this.password,
                    Adress: "г.Москва",
                    ConfirmPassword: this.password
                }, 
                {headers : 
                    {
                        'Content-Type':'application/json',
                        'Accept':'application/json'
                    }})
                .then(function (response) {
                        alert(response.data)
                })
                .catch(function (error) {
                        alert(error);
                });*/

                axios.post('http://localhost:11549/Account/login', {
                    Email: this.email,
                    Password: this.password
                },
                {
                    headers : {
                        'Content-Type':'application/json',
                        'Accept':'application/json'  
                    }
                },
                {
                    withCredentials: true
                })
                .then(function (response) {
                        alert("Вы успешно авторизовались, как " + response.data)
                })
                .catch(function () {
                        alert("Не правильный логин или пароль");
                });
            }            
    }
}

</script>
