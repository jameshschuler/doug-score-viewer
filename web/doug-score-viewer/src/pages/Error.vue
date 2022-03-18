<template>
  <div>
    <div class="columns">
      <div class="column box is-10 is-offset-1">
        <div class="p-3">
          <h1 class="is-size-3 mb-4">Error Occurred!</h1>
          <div v-if="store.error">
            <Notification :dismissible="false" :appError="store.error" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { useRouter } from "vue-router";
import Notification from "../components/Notification.vue";
import { store } from "../store";

if (!store.error) {
  useRouter().push("/");
}
</script>
<style lang="scss"></style>
