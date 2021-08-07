<template> 
        <div class="container">
            <div class="columns is-multiline is-gapless">
                <div class="column is-full">
                    <Navbar />                   
                </div>
                <div class="column is-full">
                    <Authorization />
                </div>
            </div>      
        </div>       
</template>

<script>
    import Navbar from '@/components/Navbar'
    //import Content from '@/components/Content'
    import Authorization from '@/components/Authorization'

    export default {
        components: {
            Navbar,
            //Content,
            Authorization
        }
    }

</script>

<style>
    body {
        align-content: center;
        background-color: rgb(217, 223, 235);
    }
</style>