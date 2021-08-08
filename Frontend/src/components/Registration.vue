<template>
    <div class="reg">
        <b-container>
            <b-row>

                <form action="">
                    <div class="modal-card" style="width: 500px" >
                        <header class="modal-card-head">
                            <p class="modal-card-title">Регистрация</p>
                        </header>
                        <section class="modal-card-body">
                            
                            <b-field label="Name">
                                <b-input
                                    type="name"
                                    :value="name"
                                    placeholder="Ваше имя"
                                    required
                                    v-model="name">
                                </b-input>
                            </b-field>
                            
                            <b-field label="Email">
                                <b-input
                                    type="email"
                                    :value="email"
                                    placeholder="Ваш email"
                                    required
                                    v-model="email">
                                </b-input>
                            </b-field>

                            <b-field label="Adress">
                                <b-input
                                    type="adress"
                                    :value="adress"
                                    placeholder="Ваш адрес"
                                    required
                                    v-model="adress">
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

                            <b-field label="ConfirmPassword">
                                <b-input
                                    type="password"
                                    :value="confirmPassword"
                                    password-reveal
                                    placeholder="Введите ваш пароль повторно"
                                    required
                                    v-model="confirmPassword">
                                </b-input>
                            </b-field>

                        </section>
                        <footer class="modal-card-foot">
                            <button class="button is-primary" @click="SignOn()">Зарегистрироваться</button>
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
            name:'',
            email: '',
            password: '',
            adress:'',
            confirmPassword:''
        }
    },
    methods: {
        async SignOn() {

                //alert("Регистрация пошла!");
                
                await axios.post('http://localhost:11549/Account', {
                    Name: this.name,
                    Email: this.email,
                    Password: this.password,
                    Adress: this.adress,
                    ConfirmPassword: this.confirmPassword
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
                        localStorage.setItem("token", obj.token);
                        alert("Вы успешно зарегистрировались")
                })
                .catch(function (error) {
                        alert(error);
                });               
            }            
    }
}

</script>