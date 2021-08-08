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
                        <router-link to="/reg">Зарегистрироваться</router-link>
                        <footer class="modal-card-foot">
                            <b-button variant="primary" @click="SignIn()">Войти</b-button>
                        </footer>
                    </div>
                </form>

            </b-row>         
        </b-container>               
    </div>
</template>

<script>

import axios from 'axios';
import router from '@/router';

export default {
    data() {
        return {
            email: '',
            password: ''
        }
    },
    methods: {
        SignIn() {
                
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
                .then(function(response) {
                        const obj = JSON.parse(response.data);
                        localStorage.setItem("Token", obj.Token);
                        localStorage.setItem("Name", obj.Name);
                        localStorage.setItem("Email", obj.Email);
                        localStorage.setItem("RoleId", obj.RoleId);

                        if(localStorage.getItem("RoleId") == 1) router.push("admin");
                        else if(localStorage.getItem("RoleId") == 2) router.push('customer');
                })
                .catch(function (error) {
                        alert(error);
                });
                
            }            
    }
}

</script>
