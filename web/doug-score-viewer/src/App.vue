<script setup lang="ts">
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";

checkAPIHealth();

async function checkAPIHealth() {
  const healthCheckUrl = `${import.meta.env.VITE_API_BASE_URL}/health`;
  const response = await fetch(healthCheckUrl);
  const data = await response.text();
  console.info(`API responded with...${data}`);
}
</script>

<template>
  <Navbar />
  <main class="container is-fluid my-6">
    <div class="columns">
      <router-view class="column is-12-touch"></router-view>
    </div>
  </main>
  <Footer />
</template>

<style lang="scss">
.is-red {
  color: #ff0000;

  &:hover {
    color: #ff0000;
  }
}

.has-border-great {
  border-color: hsl(141, 71%, 48%);
}

.has-border-okay {
  border-color: hsl(48, 100%, 67%);
}

.has-border-sad {
  border-color: hsl(348, 100%, 61%);
}

body {
  display: flex;
  flex-direction: column;
  min-height: 100vh;

  #app {
    flex: 1;
    display: flex;
    flex-direction: column;
  }
}
</style>
