<template>
  <div class="notification">
    <button v-if="dismissible" class="delete"></button>
    <div class="icon-text mb-1">
      <span class="icon has-text-danger">
        <i class="fa-solid fa-fw fa-lg fa-car-crash"></i>
      </span>
      <span class="is-size-5 has-text-danger has-text-weight-semibold">Error</span>
    </div>
    <p class="block has-text-weight-semibold">{{ message }}</p>
  </div>
</template>
<script setup lang="ts">
const { dismissible, message } = defineProps({
  message: String,
  dismissible: Boolean,
});
</script>
<style lang="scss"></style>
